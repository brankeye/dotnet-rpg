using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Core.Skill.CreateSkillCommand
{
    public class CreateSkillCommandHandler : ICommandHandler<CreateSkillCommand>
    {
        private readonly IMapper<CreateSkillCommand, Domain.Models.Skill> _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSkillCommandHandler(
            IMapper<CreateSkillCommand, Domain.Models.Skill> mapper,
            IUnitOfWork unitOfWork) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public Task HandleAsync(CreateSkillCommand command)
        {
            var newSkill = _mapper.Map(command);
            _unitOfWork.Skills.Create(newSkill);
            return Task.CompletedTask;
        }
    }
}