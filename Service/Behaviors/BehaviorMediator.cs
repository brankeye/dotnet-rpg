using System;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Service.Behaviors
{
    public class BehaviorMediator : IBehaviorMediator
    {
        private readonly Func<Type, dynamic> _getInstanceCallback;

        public BehaviorMediator(Func<Type, dynamic> getInstanceCallback)
        {
            _getInstanceCallback = getInstanceCallback;
        }
        
        public async Task HandleAsync(object value)
        {
            if (value == null)
            {
                return;
            }

            var valueType = value.GetType();
            var behaviorInterfaceType = typeof(IBehavior);
            var behaviorInterfaces = value.GetType().GetInterfaces()
                .Where(x => x != behaviorInterfaceType)
                .Where(x => behaviorInterfaceType.IsAssignableFrom(x));

            foreach (var behaviorType in behaviorInterfaces)
            {
                var type = typeof(IBehaviorHandler<>).MakeGenericType(behaviorType);
                var instance = _getInstanceCallback(type);

                if (instance == null)
                {
                    throw new TypeLoadException(

                        $"No behavior handler type found for operation type: {valueType.FullName}");
                }

                dynamic specificValue = Convert.ChangeType(value, valueType);
                
                await instance.HandleAsync(specificValue);
            }
        }
    }
}