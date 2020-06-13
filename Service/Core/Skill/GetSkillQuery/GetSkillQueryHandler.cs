using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Core.Skill.GetSkillQuery
{
    public class GetSkillQueryHandler : IQueryHandler<GetSkillQuery, GetSkillQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<Domain.Models.Skill, GetSkillQueryResult> _mapper;

        public GetSkillQueryHandler(
            IUnitOfWork unitOfWork, 
            IMapper<Domain.Models.Skill, GetSkillQueryResult> mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<GetSkillQueryResult> HandleAsync(GetSkillQuery query)
        {
            var skill = await _unitOfWork.Skills.Query
                .Where(x => x.UserId == query.UserId)
                .Where(x => x.Id == query.Id)
                .SingleAsync();
            return _mapper.Map(skill);
        }
    }
}