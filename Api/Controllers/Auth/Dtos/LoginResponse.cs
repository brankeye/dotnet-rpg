using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Api.Controllers.Auth.Dtos
{
    public class LoginResponse
    {
        public string Token { get; set; }
    }
}