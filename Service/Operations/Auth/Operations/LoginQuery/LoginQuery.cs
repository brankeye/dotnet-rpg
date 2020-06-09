using dotnet_rpg.Service.Contracts.CQRS.Query;

namespace dotnet_rpg.Service.Operations.Auth.Operations.LoginQuery
{
    public class LoginQuery : IQuery<LoginQueryResult>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}