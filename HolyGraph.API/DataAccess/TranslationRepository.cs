
using HolyGraph.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HolyGraph.API;

public class TranslationRepository : GraphRepositoryBase, ITranslationRepository
{

    public TranslationRepository(INeo4jGraphRepository neoGraph, AppState appState) : base(neoGraph, appState) {}

    public async Task<IEnumerable<TranslationDto>> GetTranslationsAsync()
    {
          var query =
            @"
            MATCH (n:Translation) RETURN n;
            ";
        return await _neoGraph.RunQueryAsync<TranslationDto>(query);
    }

    [HttpGet]
    public async Task<IEnumerable<BookDto>> GetTranslationBooksAsync(string translation)
    {
        Console.WriteLine("GetTranslationBooksAsync------");
          var query =
            $@"
            MATCH(t:Translation)
            MATCH(t)-[r:CONTAINS]->(b:Book)
            where t.id='{translation}'
            RETURN b,t.id as translationId;
            ";

        var bs = await _neoGraph.RunQueryAsync<BookDto>(query);

        foreach(var book in _appState.Books)
        {
            Console.WriteLine(book.bookName);
            BookDto b = bs.FirstOrDefault(x=>x.book == book.bookName);
            if(b is not null)
            {
                b.chapterCount = book.chapterCount;
                b.verseCount = book.verseCount;
                b.abbreviations = book.abbreviations;
            }
        }

        return bs;
    }

}
