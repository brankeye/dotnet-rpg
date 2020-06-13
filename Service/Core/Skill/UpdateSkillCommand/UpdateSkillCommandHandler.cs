using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Skill.UpdateSkillCommand
{
    public class UpdateSkillCommandHandler : ICommandHandler<UpdateSkillCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSkillCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task HandleAsync(UpdateSkillCommand command)
        {
            var skill = await _unitOfWork.Skills.Query
                .Where(x => x.UserId == command.UserId)
                .Where(x => x.Id == command.Id)
                .SingleAsync();
            HandleUpdate(skill, command);
            _unitOfWork.Skills.Update(skill);
        }
        
        private static void HandleUpdate(Domain.Models.Skill weapon, UpdateSkillCommand command)
        {
            weapon.Name = command.Name;
            weapon.Damage = command.Damage;
        }
    }
}