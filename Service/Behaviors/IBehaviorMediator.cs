using System.Threading.Tasks;

namespace dotnet_rpg.Service.Behaviors
{
    public interface IBehaviorMediator
    {
        Task HandleAsync(object value);
    }
}