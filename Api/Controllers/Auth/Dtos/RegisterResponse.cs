using System;

namespace dotnet_rpg.Api.Controllers.Auth.Dtos
{
    public class RegisterResponse
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
    }
}