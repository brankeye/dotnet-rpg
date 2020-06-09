using dotnet_rpg.Domain.Models;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Operations.Auth.Operations.RegisterCommand
{
    public class RegisterResponseMapper : Mapper<Domain.Models.User, RegisterCommandResponse>
    {
        protected override RegisterCommandResponse HandleMapping(Domain.Models.User input)
        {
            return new RegisterCommandResponse
            {
                Id = input.Id,
                Username = input.Username
            };
        }
    }
}