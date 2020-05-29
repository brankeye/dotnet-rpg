using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Core.User.Dtos;
using dotnet_rpg.Service.Core.User.Mapper;

namespace dotnet_rpg.Service.Core.User
{
    public class UserService : IUserService
    {
        private readonly IServiceContext _serviceContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMapper _userMapper;

        public UserService(IServiceContext serviceContext, IUnitOfWork unitOfWork)
        {
            _serviceContext = serviceContext;
            _unitOfWork = unitOfWork;
            _userMapper = new UserMapper();
        }

        public async Task<UserDto> GetAsync()
        {
            var user = await _unitOfWork.Users.Query.
                Where(x => x.Id == _serviceContext.UserId)
                .SingleAsync();
            return _userMapper.Map(user);
        }
    }
}