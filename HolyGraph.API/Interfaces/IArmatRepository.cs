namespace HolyGraph.API;

public interface IArmatRepository
{
    Task<PagedList<ArmatDto>> GetArmatsAsync(PaginationParams armatParams);
    Task<IEnumerable<ArmatDto>> GetArmatsByStartingLetterAsync(string letter, bool ignoreCase = false);
}
