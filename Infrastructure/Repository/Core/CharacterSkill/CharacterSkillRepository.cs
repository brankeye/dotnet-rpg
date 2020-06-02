using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repository.Core.CharacterSkill
{
    public class CharacterSkillRepository : Repository<Domain.Models.CharacterSkill>, ICharacterSkillRepository
    {
        public CharacterSkillRepository(DbSet<Domain.Models.CharacterSkill> dbSet) : base(dbSet)
        {
            
        }
    }
}