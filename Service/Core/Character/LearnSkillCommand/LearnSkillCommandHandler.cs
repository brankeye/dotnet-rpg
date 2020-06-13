using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Domain.Models;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Exceptions;

namespace dotnet_rpg.Service.Core.Character.LearnSkillCommand
{
    public class LearnSkillCommandHandler : ICommandHandler<LearnSkillCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LearnSkillCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task HandleAsync(LearnSkillCommand command)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == command.UserId)
                .Where(x => x.Id == command.CharacterId)
                .SingleAsync();

            if (character.CharacterSkills.Count >= 3)
            {
                throw new ServiceException("Character must unlearn a skill before learning another");
            }

            if (character.CharacterSkills.Any(x => x.SkillId == command.SkillId))
            {
                throw new ServiceException("Character has already learned this skill");
            }
            
            var skill = await _unitOfWork.Skills.Query
                .Where(x => x.UserId == command.UserId)
                .Where(x => x.Id == command.SkillId)
                .SingleAsync();

            var characterSkill = new CharacterSkill
            {
                CharacterId = character.Id,
                Character = character,
                SkillId = skill.Id,
                Skill = skill
            };
            
            _unitOfWork.CharacterSkills.Create(characterSkill);
        }
    }
}