namespace HolyGraph.API;

public interface IWordRepository
{
    Task<PagedList<ArmatDto>> GetArmatsAsync();
}
