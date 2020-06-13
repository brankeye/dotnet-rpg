using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Core.User.GetUserQuery
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, GetUserQueryResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper<Domain.Models.User, GetUserQueryResult> _mapper;

        public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper<Domain.Models.User, GetUserQueryResult> mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<GetUserQueryResult> HandleAsync(GetUserQuery query)
        {
            var user = await _unitOfWork.Users.Query.
                Where(x => x.Id == query.UserId)
                .SingleAsync();
            return _mapper.Map(user);
        }
    }
}