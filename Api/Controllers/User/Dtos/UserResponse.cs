using System;

namespace dotnet_rpg.Api.Controllers.User.Dtos
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
    }
}