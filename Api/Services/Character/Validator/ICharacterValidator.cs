using dotnet_rpg.Api.Services.Character.Dtos;
using dotnet_rpg.Api.Validation;

namespace dotnet_rpg.Api.Services.Character.Validator
{
    public interface ICharacterValidator
        : IValidator<CreateCharacterDto>,
          IValidator<UpdateCharacterDto>
    {
    
    }
}
