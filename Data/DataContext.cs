using dotnet_rpg.Domain.Models;
using dotnet_rpg.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new EnumToStringConverter<RpgClass>();
            
            modelBuilder
                .Entity<Character>()
                .Property(x => x.Class)
                .HasConversion(converter);
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Character> Characters { get; set; }

        public DbSet<Weapon> Weapons { get; set; }
    }
}