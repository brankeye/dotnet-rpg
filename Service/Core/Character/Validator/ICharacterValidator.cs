using dotnet_rpg.Service.Core.Character.Dtos;

namespace dotnet_rpg.Service.Core.Character.Validator
{
    public interface ICharacterValidator
    {
        void ValidateAndThrow(CreateCharacterDto entity);
        
        void ValidateAndThrow(UpdateCharacterDto entity);
    }
}
