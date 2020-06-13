using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Core.Character.GetCharacterQuery
{
    public class GetCharacterQueryHandler : IQueryHandler<GetCharacterQuery, GetCharacterQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<Domain.Models.Character, GetCharacterQueryResult> _mapper;

        public GetCharacterQueryHandler(
            IUnitOfWork unitOfWork, 
            IMapper<Domain.Models.Character, GetCharacterQueryResult> mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<GetCharacterQueryResult> HandleAsync(GetCharacterQuery query)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == query.UserId)
                .Where(x => x.Id == query.Id)
                .SingleAsync();
            return _mapper.Map(character);
        }
    }
}