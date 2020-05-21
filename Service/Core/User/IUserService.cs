using System.Threading.Tasks;
using dotnet_rpg.Service.Core.User.Dtos;

namespace dotnet_rpg.Service.Core.User
{
    public interface IUserService
    {
         Task<UserDto> GetAsync();
    }
}