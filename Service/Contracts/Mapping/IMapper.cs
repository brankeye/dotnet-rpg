namespace dotnet_rpg.Service.Contracts.Mapping
{
    public interface IMapper<in TInput, out TOutput>
        where TInput : class 
        where TOutput: class
    {
        TOutput Map(TInput input);
    }
}