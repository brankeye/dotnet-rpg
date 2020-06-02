using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repository.Core.Skill
{
    public class SkillRepository : Repository<Domain.Models.Skill>, ISkillRepository
    {
        public SkillRepository(DbSet<Domain.Models.Skill> dbSet) : base(dbSet)
        {
            
        }
    }
}