using System;
using dotnet_rpg.Enums;
using dotnet_rpg.Dtos;

namespace dotnet_rpg.Models
{
    public class Character
    {
        public Character() {}

        public Character(WriteCharacterDto dto) {
            if (dto == null) {
                throw new ArgumentNullException(nameof(dto));
            }

            Id = System.Guid.NewGuid();
            Name = dto.Name;
            HitPoints = 100;
            Strength = 1;
            Defense = 1;
            Intelligence = 1;
            Class = (RpgClass) Enum.Parse(typeof(RpgClass), dto.Class);
        }

        public void Update(WriteCharacterDto dto) {
            if (dto == null) {
                throw new ArgumentNullException(nameof(dto));
            }

            Name = dto.Name;
            Class = (RpgClass) Enum.Parse(typeof(RpgClass), dto.Class);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public RpgClass Class { get; set; }

        public User User { get; set; }
    }
}