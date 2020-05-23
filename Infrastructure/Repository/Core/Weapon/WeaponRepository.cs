using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repository.Core.Weapon
{
    public class WeaponRepository : Repository<Domain.Models.Weapon>, IWeaponRepository
    {
        public WeaponRepository(DbSet<Domain.Models.Weapon> dbSet) : base(dbSet)
        {
            
        }
    }
}