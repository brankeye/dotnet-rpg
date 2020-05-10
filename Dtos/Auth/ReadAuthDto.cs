using System;

namespace dotnet_rpg.Dtos.Auth
{
    public class ReadAuthDto
    {
        public ReadAuthDto() {}

        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}