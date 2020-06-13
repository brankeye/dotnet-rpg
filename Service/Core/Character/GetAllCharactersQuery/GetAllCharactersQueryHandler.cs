using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Contracts.Mapping;
using dotnet_rpg.Service.Core.Character.GetCharacterQuery;

namespace dotnet_rpg.Service.Core.Character.GetAllCharactersQuery
{
    public class GetAllCharactersQueryHandler : IQueryHandler<GetAllCharactersQuery, IEnumerable<GetCharacterQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<Domain.Models.Character, GetCharacterQueryResult> _mapper;

        public GetAllCharactersQueryHandler(
            IUnitOfWork unitOfWork, 
            IMapper<Domain.Models.Character, GetCharacterQueryResult> mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCharacterQueryResult>> HandleAsync(GetAllCharactersQuery query)
        {
            var characters = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == query.UserId)
                .ToListAsync();
            return characters.Select(_mapper.Map);
        }
    }
}