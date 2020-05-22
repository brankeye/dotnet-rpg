using dotnet_rpg.Data;
using dotnet_rpg.Domain.Models;

namespace dotnet_rpg.Infrastructure.Repositories.CharacterWeaponRepository
{
    public class CharacterWeaponRepository : Repository<CharacterWeapon>, ICharacterWeaponRepository
    {
        private readonly DataContext _dataContext;
        
        public CharacterWeaponRepository(DataContext dataContext) : base(dataContext.CharacterWeapons)
        {
            _dataContext = dataContext;
        }
    }
}