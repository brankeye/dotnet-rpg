using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using dotnet_rpg.Domain.Data;
using dotnet_rpg.Domain.Models;
using dotnet_rpg.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));

            if (user == null)
            {
                throw new NotFoundException(nameof(username), username);
            }

            return user;
        }

        public async Task<User> CreateAsync(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser));
            }

            newUser.Id = Guid.NewGuid();
            var user = await _dataContext.Users.AddAsync(newUser);

            return user.Entity;
        }

        public async Task<bool> ExistsAsync(string username)
        {
            var exists = await _dataContext.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
            return exists;
        }
    }
}
