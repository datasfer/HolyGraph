using HolyGraph.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HolyGraph.API;

public class BooksController : BaseApiController
{
    private readonly IBookRepository _bookRepository;

    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }


    /// <summary>
    /// Get scriptures
    /// </summary>
    /// <param name="scripture">All, Bible, Narek</param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    // curl -X GET "http://195.201.22.161:33010/Scripture/books" -H "accept: */*"
    // curl -X GET "http://localhost:33010/Scripture/books" -H "accept: */*"
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> getScriptures([FromQuery] BookParams bookParams)
    {
        var bs = await _bookRepository.GetBooksAsync(bookParams);

        Console.WriteLine(bookParams);
        Response.AddPaginationHeader(new PaginationHeader(bs.CurrentPage, bs.PageSize, bs.TotalCount, bs.TotalPages));

        return Ok(bs);
    }

}
