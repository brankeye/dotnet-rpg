using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Exceptions;

namespace dotnet_rpg.Service.Core.Character.UnequipSkillCommand
{
    public class UnlearnSkillCommandHandler : ICommandHandler<UnlearnSkillCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnlearnSkillCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task HandleAsync(UnlearnSkillCommand command)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == command.UserId)
                .Where(x => x.Id == command.CharacterId)
                .SingleAsync();

            if (character.CharacterSkills.Count == 0)
            {
                throw new ServiceException("Character has no skills");
            }

            if (character.CharacterSkills.All(x => x.SkillId != command.SkillId))
            {
                throw new ServiceException("Character has not learned this skill");
            }
            
            var characterSkill = await _unitOfWork.CharacterSkills.Query
                .Where(x => x.CharacterId == command.CharacterId)
                .Where(x => x.SkillId == command.SkillId)
                .SingleAsync();

            _unitOfWork.Characters.Update(character);
            _unitOfWork.CharacterSkills.Delete(characterSkill);
        }
    }
}