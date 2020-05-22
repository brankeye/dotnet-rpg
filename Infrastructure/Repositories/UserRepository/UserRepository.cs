using System;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Domain.Models;
using dotnet_rpg.Infrastructure.Exceptions;
using dotnet_rpg.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repositories.UserRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext) : base(dataContext.Users)
        {
            _dataContext = dataContext;
        }
    }
}
