using System;
using System.Threading.Tasks;

namespace dotnet_rpg.Service.Behaviors.GeneratedId
{
    public class GeneratedIdBehaviorHandler : IBehaviorHandler<IGeneratedIdBehavior>
    {
        public GeneratedIdBehaviorHandler()
        {
            
        }

        public Task HandleAsync(IGeneratedIdBehavior value)
        {
            value.GeneratedId = Guid.NewGuid();
            return Task.CompletedTask;
        }
    }
}