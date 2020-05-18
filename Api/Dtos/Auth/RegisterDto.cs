using System;

namespace dotnet_rpg.Api.Dtos.Auth
{
    public class RegisterDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
    }
}