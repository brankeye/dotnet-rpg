using System;
using dotnet_rpg.Enums;
using dotnet_rpg.Models;

namespace dotnet_rpg.Dtos
{
    public class ReadCharacterDto
    {
        public ReadCharacterDto() {}

        public ReadCharacterDto(Character model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            Id = model.Id;
            Name = model.Name;
            HitPoints = model.HitPoints;
            Strength = model.Strength;
            Defense = model.Defense;
            Intelligence = model.Intelligence;
            Class = model.Class.ToString();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public string Class { get; set; }
    }
}