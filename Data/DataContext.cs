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
            modelBuilder
                .Entity<Character>()
                .Property(x => x.Class)
                .HasConversion(new EnumToStringConverter<RpgClass>());

            modelBuilder.Entity<CharacterSkill>()
                .HasKey(x => new {x.CharacterId, x.SkillId});

            modelBuilder.Entity<CharacterSkill>()
                .HasIndex(x => x.SkillId)
                .IsUnique();
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Character> Characters { get; set; }

        public DbSet<Weapon> Weapons { get; set; }
        
        public DbSet<Skill> Skills { get; set; }
        
        public DbSet<CharacterSkill> CharacterSkills { get; set; }
    }
}