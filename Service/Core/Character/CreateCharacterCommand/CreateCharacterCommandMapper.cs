using System;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Core.Character.CreateCharacterCommand
{
    public class CreateCharacterCommandMapper : IMapper<CreateCharacterCommand, Domain.Models.Character>
    {
        public Domain.Models.Character Map(CreateCharacterCommand input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return new Domain.Models.Character
            {
                UserId = input.UserId,
                Id = input.GeneratedId,
                Name = input.Name,
                HitPoints = 100,
                Strength = 1,
                Defense = 1,
                Intelligence = 1,
                Class = (RpgClass) Enum.Parse(typeof(RpgClass), input.Class)
            };
        }
    }
}