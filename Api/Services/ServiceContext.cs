using System;
using System.Security.Claims;
using dotnet_rpg.Api.Services;
using Microsoft.AspNetCore.Http;

namespace dotnet_rpg.Api.Services
{
    public class ServiceContext : IServiceContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId => Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}