namespace dotnet_rpg.Service.Contracts.Mapping
{
    public interface IMapper<in TInput, out TOutput>
    {
        TOutput Map(TInput input);
    }
}