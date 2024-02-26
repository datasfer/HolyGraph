namespace HolyGraph.API;

public interface IVerseRepository
{
    Task<IEnumerable<VerseDto>> GetVerseRangeAsync(VerseParams verseParams);

    /// <summary>
    /// return paged verses without sections, via chapters.
    /// </summary>
    /// <param name="verseParams"></param>
    /// <returns></returns>
    Task<PagedList<VerseDto>> GetVersesAsync(VerseParams verseParams);

    /// <summary>
    ///  return pagad vesres with section title, pages mean by skip and limit.
    /// </summary>
    /// <param name="verseParams"></param>
    /// <returns></returns>
    Task<PagedList<VerseDto>> GetSectionedVersesAsync(VerseParams verseParams);

    /// <summary>
    /// Get all verses of the chapter
    /// </summary>
    /// <param name="translation"></param>
    /// <param name="book"></param>
    /// <param name="chapter"></param>
    /// <param name="withSections"></param>
    /// <returns></returns>
    Task<IEnumerable<VerseDto>> GetChapterVersesAsync(string translation, string book, int chapter, bool withSections = false);
    Task<VerseDto> GetVerseAsync(VerseParams verseParams);
    
}
