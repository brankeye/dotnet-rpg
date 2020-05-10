using System.Security.Authentication;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Auth;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<ReadAuthDto>> LoginAsync(string username, string password) {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<ReadAuthDto>> RegisterAsync(User user, string password) {
            if (await UserExistsAsync(user.Username))
            {
                throw new AuthenticationException("User already exists.");
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var serviceResponse = new ServiceResponse<ReadAuthDto>(new ReadAuthDto {
                Id = user.Id,
                Username = user.Username
            });
            return serviceResponse;
        }

        public async Task<bool> UserExistsAsync(string username) {
            if (await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}