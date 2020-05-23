using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repository.Core.CharacterWeapon
{
    public class CharacterWeaponRepository : Repository<Domain.Models.CharacterWeapon>, ICharacterWeaponRepository
    {
        public CharacterWeaponRepository(DbSet<Domain.Models.CharacterWeapon> dbSet) : base(dbSet)
        {
            
        }
    }
}