using System.Security.Authentication;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Contracts.Mapping;
using dotnet_rpg.Service.Utility.AuthUtility;

namespace dotnet_rpg.Service.Core.Auth.RegisterCommand
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly IMapper<CryptographicPassword, Domain.Models.User> _userMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthUtility _authUtility;

        public RegisterCommandHandler(
            IMapper<CryptographicPassword, Domain.Models.User> userMapper,
            IUnitOfWork unitOfWork,
            IAuthUtility authUtility) 
        {
            _userMapper = userMapper;
            _unitOfWork = unitOfWork;
            _authUtility = authUtility;
        }
        
        public async Task HandleAsync(Auth.RegisterCommand.RegisterCommand command)
        {
            var userExists = await _unitOfWork.Users.Query
                .Where(x => x.Username == command.Username)
                .ExistsAsync();
            
            if (userExists)
            {
                throw new AuthenticationException("User already exists.");
            }

            var cryptographicPassword = _authUtility.CreatePasswordHash(command.Password);

            var newUser = _userMapper.Map(cryptographicPassword);

            newUser.Id = command.GeneratedId;
            newUser.Username = command.Username;
            
            _unitOfWork.Users.Create(newUser);
        }
    }
}