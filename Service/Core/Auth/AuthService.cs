using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Core.Auth.Dtos;
using dotnet_rpg.Service.Core.Auth.Mapper;
using dotnet_rpg.Service.Core.Auth.Validator;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_rpg.Service.Core.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthValidator _authValidator;
        private readonly IAuthMapper _authMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(
            IAuthValidator authValidator, 
            IUnitOfWork unitOfWork, 
            IConfiguration configuration) 
        {
            _authValidator = authValidator;
            _authMapper = new AuthMapper();
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<LoginDto> LoginAsync(CredentialsDto dto) {
            _authValidator.ValidateAndThrow(dto);

            var user = await _unitOfWork.Users.Query
                .Where(x => x.Username == dto.Username)
                .SingleAsync();

            if (user == null) {
                throw new AuthenticationException("User not found.");
            } 
            
            if (!VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt)) {
                throw new AuthenticationException("Authentication failed.");
            }

            var token = CreateToken(user);

            return _authMapper.Map(token);
        }

        public async Task<RegisterDto> RegisterAsync(CredentialsDto dto) {
            _authValidator.ValidateAndThrow(dto);

            var userExists = await _unitOfWork.Users.Query
                .Where(x => x.Username == dto.Username)
                .ExistsAsync();
            
            if (userExists)
            {
                throw new AuthenticationException("User already exists.");
            }

            CreatePasswordHash(dto.Password, out var passwordHash, out var passwordSalt);

            var newUser = _authMapper.Map(dto, passwordHash, passwordSalt);
            var user = _unitOfWork.Users.Create(newUser);
            await _unitOfWork.CommitAsync();

            return _authMapper.Map(user);
        }
        
        private JwtSecurityToken CreateToken(Domain.Models.User user) {
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
            var jwt = tokenHandler.WriteToken(token);
            return tokenHandler.ReadJwtToken(jwt);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, IReadOnlyList<byte> passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return !computedHash.Where((b, i) => b != passwordHash[i]).Any();
        }
    }
}