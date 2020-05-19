using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotnet_rpg.Api.Dtos.Auth;
using dotnet_rpg.Api.Services.Auth.Validator;
using dotnet_rpg.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_rpg.Api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IAuthValidator _authValidator;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration) {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _authValidator = new AuthValidator();
        }

        public async Task<LoginDto> LoginAsync(CredentialsDto dto) {
            _authValidator.Validate(dto);

            var user = await _unitOfWork.Users.GetByUsernameAsync(dto.Username);

            if (user == null) {
                throw new AuthenticationException("User not found.");
            } else if (!VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt)) {
                throw new AuthenticationException("Authentication failed.");
            }

            var token = CreateToken(user);

            var response = new LoginDto {
                Token = token
            };

            return response;
        }

        public async Task<RegisterDto> RegisterAsync(CredentialsDto dto) {
            _authValidator.Validate(dto);

            if (await UserExistsAsync(dto.Username))
            {
                throw new AuthenticationException("User already exists.");
            }

            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new Domain.Models.User {
                Username = dto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var userRecord = await _unitOfWork.Users.CreateAsync(user);
            await _unitOfWork.CommitAsync();

            var response = new RegisterDto {
                Id = userRecord.Id,
                Username = userRecord.Username
            };

            return response;
        }

        public async Task<bool> UserExistsAsync(string username) {
            return await _unitOfWork.Users.ExistsAsync(username);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++) {
                    if (computedHash[i] != passwordHash[i]) {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CreateToken(Domain.Models.User user) {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}