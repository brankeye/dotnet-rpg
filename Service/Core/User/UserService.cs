using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Core.Auth.Dtos;
using dotnet_rpg.Service.Core.Auth.Validator;
using dotnet_rpg.Service.Core.User.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_rpg.Service.Core.User
{
    public class UserService : IUserService
    {
        private readonly IServiceContext _serviceContext;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IServiceContext serviceContext, IUnitOfWork unitOfWork)
        {
            _serviceContext = serviceContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> GetAsync()
        {
            var user = await _unitOfWork.Users.GetByIdAsync(_serviceContext.UserId);

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username
            };
        }
    }
}