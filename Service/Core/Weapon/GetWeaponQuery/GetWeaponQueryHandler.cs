using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Contracts.Mapping;
using dotnet_rpg.Service.Core.Character.GetCharacterQuery;

namespace dotnet_rpg.Service.Core.Weapon.GetWeaponQuery
{
    public class GetWeaponQueryHandler : IQueryHandler<GetWeaponQuery, GetWeaponQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<Domain.Models.Weapon, GetWeaponQueryResult> _mapper;

        public GetWeaponQueryHandler(
            IUnitOfWork unitOfWork, 
            IMapper<Domain.Models.Weapon, GetWeaponQueryResult> mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<GetWeaponQueryResult> HandleAsync(GetWeaponQuery query)
        {
            var weapon = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == query.UserId)
                .Where(x => x.Id == query.Id)
                .SingleAsync();
            return _mapper.Map(weapon);
        }
    }
}