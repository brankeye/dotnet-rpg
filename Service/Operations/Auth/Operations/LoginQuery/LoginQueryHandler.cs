using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Contracts.Mapping;
using dotnet_rpg.Service.Contracts.Validation;
using dotnet_rpg.Service.Utility.AuthUtility;

namespace dotnet_rpg.Service.Operations.Auth.Operations.LoginQuery
{
    public class LoginQueryHandler : IQueryHandler<LoginQuery, LoginQueryResult>
    {
        private readonly IMapper<JwtSecurityToken, LoginQueryResult> _loginResponseMapper;
        private readonly IAuthUtility _authUtility;
        private readonly IUnitOfWork _unitOfWork;
        
        public LoginQueryHandler(
            IMapper<JwtSecurityToken, LoginQueryResult> loginResponseMapper,
            IAuthUtility authUtility,
            IUnitOfWork unitOfWork) 
        {
            _loginResponseMapper = loginResponseMapper;
            _authUtility = authUtility;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<LoginQueryResult> HandleAsync(LoginQuery query)
        {
            var user = await _unitOfWork.Users.Query
                .Where(x => x.Username == query.Username)
                .SingleAsync();

            if (user == null) {
                throw new AuthenticationException("User not found.");
            } 
            
            if (!_authUtility.VerifyPasswordHash(query.Password, user.PasswordHash, user.PasswordSalt)) {
                throw new AuthenticationException("Authentication failed.");
            }

            var token = _authUtility.CreateToken(user);

            return _loginResponseMapper.Map(token);
        }
    }
}