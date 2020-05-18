using System;
using dotnet_rpg.Api.Dtos.Character;

namespace dotnet_rpg.Api.Validators.CharacterValidator
{
    public interface ICharacterValidator
        : IValidator<CreateCharacterDto>,
          IValidator<UpdateCharacterDto>
    {
    
    }
}
