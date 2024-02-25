
using HolyGraph.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HolyGraph.API;

public class BookRepository : IBookRepository
{
    private readonly INeo4jGraphRepository _neoGraph;
    private readonly AppState _appState;

    public BookRepository(INeo4jGraphRepository neoGraph, AppState appState)
    {
        _neoGraph = neoGraph;
        _appState = appState;
    }   
    public Task<BookDto> GetBookAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedList<BookDto>> GetBooksAsync(BookParams bookParams)
    {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Request: Book -> getScriptures");
        
        string where = string.Empty;
        if (bookParams.Scripture != ScriptureEnum.All)
        {
            where = $"WHERE b.Label = '{bookParams.Scripture}";
        }

        if (!string.IsNullOrEmpty(bookParams.Translation))
        {
            where = $"WHERE t.id = '{bookParams.Translation}";
        }



        var query =
            $@"
            MATCH(b:Book)
            MATCH(t:Translation)<-[r:CONTAINS]->(b)
            {where}
            RETURN b,t.id as translationId 
            <skip> 
            <limit>;
            ";

        var skip = (bookParams.PageNumber - 1) * bookParams.PageSize;

        query = query.Replace("<skip>",  $"SKIP  {skip}");
        query = query.Replace("<limit>", $"LIMIT {bookParams.PageSize}");

        var books = await _neoGraph.RunQueryAsync<BookDto>(query);

        // var jsonResult = await _grapthRepository.RunQueryAsync(query);
        // return Content(jsonResult, "application/json");

        //  var book = _appState
        //                 .Books
        //                 .Where(b => b.bookName.Equals(bookParams.book, StringComparison.OrdinalIgnoreCase))
        //                 .SingleOrDefault();

        // var chapter = book?.chapters[verseParams.Chapter];
        
        var pagedList = new PagedList<BookDto>(books, 133, bookParams.PageNumber, bookParams.PageSize);

        return pagedList;
    }


    
}
