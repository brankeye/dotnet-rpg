using dotnet_rpg.Api.Controllers.Auth.Dtos;
using dotnet_rpg.Api.Controllers.Character.Dtos;
using dotnet_rpg.Api.Mapper;
using dotnet_rpg.Api.Services.Character.Dtos;
using dotnet_rpg.Service.Core.Auth.Dtos;
using dotnet_rpg.Service.Core.Character.Dtos;

namespace dotnet_rpg.Api.Controllers.Character.Mapper
{
    public interface ICharacterMapper : 
        IMapper<CharacterDto, CharacterResponse>,
        IMapper<CreateCharacterRequest, CreateCharacterDto>,
        IMapper<UpdateCharacterRequest, UpdateCharacterDto>
    {
        
    }
}