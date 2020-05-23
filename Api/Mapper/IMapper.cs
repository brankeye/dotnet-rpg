namespace dotnet_rpg.Api.Mapper
{
    public interface IMapper<in TSource, out TDest>
    {
        TDest Map(TSource source);
    }
}