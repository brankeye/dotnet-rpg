using System;
using System.Threading.Tasks;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Contracts.CQRS.Query;

namespace dotnet_rpg.Service.Contracts.CQRS.Mediator
{
    public class Mediator : IMediator
    {
        private readonly Func<Type, dynamic> _getInstanceCallback;

        public Mediator(Func<Type, dynamic> getInstanceCallback)
        {
            _getInstanceCallback = getInstanceCallback;
        }
        
        public Task<TResult> HandleAsync<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();
            var type = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));
            var instance = _getInstanceCallback(type);

            if (instance == null)
            {
                throw new TypeLoadException(

                    $"No query handler type found for query type: {queryType.FullName}");
            }

            dynamic specificQuery = Convert.ChangeType(query, queryType);

            return instance.HandleAsync(specificQuery);
        }

        public Task HandleAsync(ICommand command)
        {
            var commandType = command.GetType();
            var type = typeof(ICommandHandler<>).MakeGenericType(commandType);
            var instance = _getInstanceCallback(type);

            if (instance == null)
            {
                throw new TypeLoadException(

                    $"No command handler type found for command type: {commandType.FullName}");
            }

            dynamic specificCommand = Convert.ChangeType(command, commandType);

            return instance.HandleAsync(specificCommand);
        }
    }
}