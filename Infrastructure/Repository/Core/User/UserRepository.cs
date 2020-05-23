using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repository.Core.User
{
    public class UserRepository : Repository<Domain.Models.User>, IUserRepository
    {
        public UserRepository(DbSet<Domain.Models.User> dbSet) : base(dbSet)
        {
            
        }
    }
}
