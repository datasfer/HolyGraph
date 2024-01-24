namespace HolyGraph.API;

public interface IVerseRepository
{
    Task<PagedList<VerseDto>> GetVerseRangeAsync(VerseParams verseParams);
    Task<VerseDto> GetVerseAsync(VerseParams verseParams);
}
