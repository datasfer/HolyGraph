namespace HolyGraph.API;

public interface ITranslationRepository
{
    Task<IEnumerable<TranslationDto>> GetTranslationsAsync();
    Task<IEnumerable<BookDto>> GetTranslationBooksAsync(string translation);
}
