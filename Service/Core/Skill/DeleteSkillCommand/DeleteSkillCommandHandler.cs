using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Skill.DeleteSkillCommand
{
    public class DeleteSkillCommandHandler : ICommandHandler<DeleteSkillCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSkillCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task HandleAsync(DeleteSkillCommand command)
        {
            var skill = await _unitOfWork.Skills.Query
                .Where(x => x.UserId == command.UserId)
                .Where(x => x.Id == command.Id)
                .SingleAsync();
            _unitOfWork.Skills.Delete(skill);
        }
    }
}