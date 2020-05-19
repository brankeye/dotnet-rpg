using System.Threading.Tasks;
using dotnet_rpg.Api.Dtos.Auth;

namespace dotnet_rpg.Api.Services.Auth
{
    public interface IAuthService
    {
         Task<RegisterDto> RegisterAsync(CredentialsDto dto);
         Task<LoginDto> LoginAsync(CredentialsDto dto);
         Task<bool> UserExistsAsync(string username);
    }
}