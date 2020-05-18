using System;
using System.Collections.Generic;

namespace dotnet_rpg.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        
        public List<Character> Characters { get; set; }
    }
}