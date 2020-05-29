using dotnet_rpg.Api.Controllers.Character.Dtos;
using dotnet_rpg.Api.Services.Character.Dtos;
using dotnet_rpg.Service.Core.Character.Dtos;

namespace dotnet_rpg.Api.Controllers.Character.Mapper
{
    public interface ICharacterMapper
    {
        CharacterResponse Map(CharacterDto dto);
        
        CreateCharacterDto Map(CreateCharacterRequest request);
        
        UpdateCharacterDto Map(UpdateCharacterRequest request);
    }
}