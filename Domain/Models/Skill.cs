using System;

namespace dotnet_rpg.Domain.Models
{
    public class Skill
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public int Damage { get; set; }
        
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}