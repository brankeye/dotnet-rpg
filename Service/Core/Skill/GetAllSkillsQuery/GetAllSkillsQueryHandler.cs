using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Contracts.Mapping;
using dotnet_rpg.Service.Core.Skill.GetSkillQuery;

namespace dotnet_rpg.Service.Core.Skill.GetAllSkillsQuery
{
    public class GetAllSkillsQueryHandler : IQueryHandler<GetAllSkillsQuery, IEnumerable<GetSkillQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<Domain.Models.Skill, GetSkillQueryResult> _mapper;

        public GetAllSkillsQueryHandler(
            IUnitOfWork unitOfWork, 
            IMapper<Domain.Models.Skill, GetSkillQueryResult> mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetSkillQueryResult>> HandleAsync(GetAllSkillsQuery query)
        {
            var skills = await _unitOfWork.Skills.Query
                .Where(x => x.UserId == query.UserId)
                .ToListAsync();
            return skills.Select(_mapper.Map);
        }
    }
}