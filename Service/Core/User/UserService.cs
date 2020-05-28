using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Core.User.Dtos;

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
            var user = await _unitOfWork.Users.Query.
                Where(x => x.Id == _serviceContext.UserId)
                .SingleAsync();

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username
            };
        }
    }
}