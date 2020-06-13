using System;
using System.Security.Claims;
using dotnet_rpg.Service.Contracts.Context;
using Microsoft.AspNetCore.Http;

namespace dotnet_rpg.Api.Context
{
    public class AuthContext : IAuthContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId => Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        public string Username => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
    }
}