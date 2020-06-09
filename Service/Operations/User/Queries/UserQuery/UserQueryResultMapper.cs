using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Operations.User.Queries.UserQuery
{
    public class UserQueryResultMapper : IMapper<Domain.Models.User, UserQueryResult>
    {
        public UserQueryResult Map(Domain.Models.User input)
        {
            return new UserQueryResult
            {
                Id = input.Id,
                Username = input.Username
            };
        }
    }
}