using System.Threading.Tasks;
using dotnet_rpg.Service.Core.Auth.Dtos;

namespace dotnet_rpg.Service.Core.Auth
{
    public interface IAuthService
    {
         Task<RegisterDto> RegisterAsync(CredentialsDto dto);
         Task<LoginDto> LoginAsync(CredentialsDto dto);
         Task<bool> UserExistsAsync(string username);
    }
}