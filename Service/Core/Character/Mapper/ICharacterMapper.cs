using System;
using dotnet_rpg.Service.Core.Character.Dtos;

namespace dotnet_rpg.Service.Core.Character.Mapper
{
    public interface ICharacterMapper
    {
        CharacterDto Map(Domain.Models.Character character);
        
        Domain.Models.Character Map(CreateCharacterDto dto, Guid userId);
    }
}