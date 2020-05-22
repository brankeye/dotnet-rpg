using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Domain.Models;
using dotnet_rpg.Infrastructure.Exceptions;
using dotnet_rpg.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repositories.WeaponRepository
{
    public class WeaponRepository : Repository<Weapon>, IWeaponRepository
    {
        private readonly DataContext _dataContext;

        public WeaponRepository(DataContext dataContext) : base(dataContext.Weapons)
        {
            _dataContext = dataContext;
        }
    }
}