using System.Threading.Tasks;

namespace dotnet_rpg.Service.Behaviors
{
    public interface IBehaviorHandler<in TProperty>
        where TProperty : IBehavior
    {
        Task HandleAsync(TProperty value);
    }
}