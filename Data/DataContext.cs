using dotnet_rpg.Enums;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            var converter = new EnumToStringConverter<RpgClass>();
            
            modelBuilder
                .Entity<Character>()
                .Property(e => e.Class)
                .HasConversion(converter);
        }
        
        public DbSet<Character> Characters { get; set; }

        public DbSet<User> Users { get; set; }
    }
}