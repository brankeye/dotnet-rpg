using System.Threading.Tasks;
using dotnet_rpg.Dtos.Auth;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.AuthService
{
    public interface IAuthService
    {
         Task<ServiceResponse<ReadAuthDto>> RegisterAsync(User user, string password);
         Task<ServiceResponse<ReadAuthDto>> LoginAsync(string username, string password);
         Task<bool> UserExistsAsync(string username);
    }
}