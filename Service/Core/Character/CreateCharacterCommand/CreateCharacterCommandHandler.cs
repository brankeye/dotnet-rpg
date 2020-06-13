using System.Security.Authentication;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Core.Character.CreateCharacterCommand
{
    public class CreateCharacterCommandHandler : ICommandHandler<CreateCharacterCommand>
    {
        private readonly IMapper<CreateCharacterCommand, Domain.Models.Character> _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCharacterCommandHandler(
            IMapper<CreateCharacterCommand, Domain.Models.Character> mapper,
            IUnitOfWork unitOfWork) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public Task HandleAsync(CreateCharacterCommand command)
        {
            var newCharacter = _mapper.Map(command);
            _unitOfWork.Characters.Create(newCharacter);
            return Task.CompletedTask;
        }
    }
}