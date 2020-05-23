using System;
using dotnet_rpg.Api.Controllers.Auth.Dtos;
using dotnet_rpg.Api.Controllers.Auth.Mapper;
using dotnet_rpg.Api.Controllers.Character.Dtos;
using dotnet_rpg.Api.Controllers.Weapon.Mapper;
using dotnet_rpg.Api.Services.Character.Dtos;
using dotnet_rpg.Service.Core.Auth.Dtos;
using dotnet_rpg.Service.Core.Character.Dtos;

namespace dotnet_rpg.Api.Controllers.Character.Mapper
{
    public class CharacterMapper : ICharacterMapper
    {
        private readonly IWeaponMapper _weaponMapper;
        
        public CharacterMapper()
        {
            _weaponMapper = new WeaponMapper();
        }
        
        public CharacterResponse Map(CharacterDto source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new CharacterResponse
            {
                Id = source.Id,
                Name = source.Name,
                HitPoints = source.HitPoints,
                Strength = source.Strength,
                Defense = source.Defense,
                Intelligence = source.Intelligence,
                Class = source.Class,
                Weapon = _weaponMapper.Map(source.Weapon)
            };
        }

        public CreateCharacterDto Map(CreateCharacterRequest source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new CreateCharacterDto
            {
                Name = source.Name,
                Class = source.Class
            };
        }

        public UpdateCharacterDto Map(UpdateCharacterRequest source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new UpdateCharacterDto
            {
                Name = source.Name,
                Class = source.Class
            };
        }
    }
}