namespace HolyGraph.API;

public interface IBookRepository
{
    Task<PagedList<BookDto>> GetBooksAsync(BookParams bookParams);
    Task<BookDto> GetBookAsync(string username);
}
