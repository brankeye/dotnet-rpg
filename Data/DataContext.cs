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
            
            modelBuilder.Entity<CharacterWeapon>()
                .HasKey(x => new { x.CharacterId, x.WeaponId });
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Character> Characters { get; set; }

        public DbSet<Weapon> Weapons { get; set; }
        
        public DbSet<CharacterWeapon> CharacterWeapons { get; set; }
    }
}