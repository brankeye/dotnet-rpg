using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repository.Core.Character
{
    public class CharacterRepository : Repository<Domain.Models.Character>, ICharacterRepository
    {
        public CharacterRepository(DbSet<Domain.Models.Character> dbSet) : base(dbSet)
        {
            
        }

        protected override IQueryable<Domain.Models.Character> ModifyQuery(IQueryable<Domain.Models.Character> queryable)
        {
            return queryable
                .Include(x => x.Weapon)
                .Include(x => x.CharacterSkills)
                .ThenInclude(x => x.Skill);
        }
    }
}