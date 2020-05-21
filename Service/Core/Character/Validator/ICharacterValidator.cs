using dotnet_rpg.Api.Services.Character.Dtos;
using dotnet_rpg.Service.Core.Character.Dtos;
using dotnet_rpg.Service.Validation;

namespace dotnet_rpg.Service.Core.Character.Validator
{
    public interface ICharacterValidator
        : IValidator<CreateCharacterDto>,
          IValidator<UpdateCharacterDto>
    {
    
    }
}
