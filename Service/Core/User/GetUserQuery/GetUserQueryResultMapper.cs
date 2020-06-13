using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Core.User.GetUserQuery
{
    public class GetUserQueryResultMapper : IMapper<Domain.Models.User, GetUserQueryResult>
    {
        public GetUserQueryResult Map(Domain.Models.User input)
        {
            return new GetUserQueryResult
            {
                Id = input.Id,
                Username = input.Username
            };
        }
    }
}