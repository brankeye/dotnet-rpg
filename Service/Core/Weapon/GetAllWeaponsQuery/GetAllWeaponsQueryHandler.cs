using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Contracts.Mapping;
using dotnet_rpg.Service.Core.Character.GetAllCharactersQuery;
using dotnet_rpg.Service.Core.Character.GetCharacterQuery;
using dotnet_rpg.Service.Core.Weapon.GetWeaponQuery;

namespace dotnet_rpg.Service.Core.Weapon.GetAllWeaponsQuery
{
    public class GetAllWeaponsQueryHandler : IQueryHandler<GetAllWeaponsQuery, IEnumerable<GetWeaponQueryResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<Domain.Models.Weapon, GetWeaponQueryResult> _mapper;

        public GetAllWeaponsQueryHandler(
            IUnitOfWork unitOfWork, 
            IMapper<Domain.Models.Weapon, GetWeaponQueryResult> mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetWeaponQueryResult>> HandleAsync(GetAllWeaponsQuery query)
        {
            var weapons = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == query.UserId)
                .ToListAsync();
            return weapons.Select(_mapper.Map);
        }
    }
}