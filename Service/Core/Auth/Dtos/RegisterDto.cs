using System;

namespace dotnet_rpg.Service.Core.Auth.Dtos
{
    public class RegisterDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
    }
}