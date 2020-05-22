using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using dotnet_rpg.Domain.Models;
using dotnet_rpg.Infrastructure.Extensions;

namespace dotnet_rpg.Infrastructure.Repositories.CharacterRepository
{
    public class CharacterRepository : Repository<Character>, ICharacterRepository
    {
        private readonly DataContext _dataContext;

        public CharacterRepository(DataContext dataContext) : base(dataContext.Characters)
        {
            _dataContext = dataContext;
        }
    }
}