using System;
using System.Collections.Generic;

namespace dotnet_rpg.Models
{
    public class User
    {
        public User() {
            Id = System.Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<Character> Characters { get; set; }
    }
}