using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Operations.User.Queries.UserQuery
{
    public class UserQueryHandler : IQueryHandler<UserQuery, UserQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<Domain.Models.User, UserQueryResult> _mapper;

        public UserQueryHandler(IUnitOfWork unitOfWork, IMapper<Domain.Models.User, UserQueryResult> mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<UserQueryResult> HandleAsync(UserQuery query)
        {
            var user = await _unitOfWork.Users.Query.
                Where(x => x.Id == query.UserId)
                .SingleAsync();
            return _mapper.Map(user);
        }
    }
}