using System;
using System.Threading.Tasks;
using dotnet_rpg.Domain.Models;

namespace dotnet_rpg.Infrastructure.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User entity);
        Task<bool> ExistsAsync(string username);
    }
}
