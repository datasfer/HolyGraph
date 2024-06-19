using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HolyGraph.API.Extensions;
using HolyGraph.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace HolyGraph.API.Controllers;

public class ScripturesController : BaseApiController
{
    private readonly INeo4jGraphRepository _grapthRepository;
    private readonly ILogger<ScripturesController> _logger;

    public ScripturesController(INeo4jGraphRepository grapthRepository, ILogger<ScripturesController> logger)
    {
        _grapthRepository = grapthRepository;
        _logger = logger;
    }

    #region Meta information

    /// <summary>
    /// verse number of chapter of the book 
    /// </summary>
    /// <param name="translation"></param>
    /// <param name="book"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    [HttpGet("{translation}/books/{book}/chapters/{num}/verses/count")]
    public IActionResult getBookChapterVerses(string translation, string book, int num)
    {
        var query =
        $"MATCH(t:Translation) where t.id = '{translation}' " +
        $"match (b:Book {{book: '{book}' }})-[:CONTAINS]-()-[*]->(v:Verse) " +
        $"WHERE v.chapter={num} " +
        $"return Max(v.verse) as num";

        //return await QueryAndReturnJsonAsync(query);
        return Content(100.ToString(), "application/json");
    }

    /// <summary>
    /// chapter number of the book
    /// </summary>
    /// <param name="book"></param>
    /// <param name="translation"></param>
    /// <returns></returns>
    [HttpGet("{translation}/books/{book}/chapters/count")]
    public async Task<IActionResult> getBookChaptersCount(string translation, string book)
    {
        var query =
            $"MATCH(t:Translation) where t.id = '{translation}' " +
            $"match (b:Book {{book: '{book}' }})-->(v:Chapter) " +
            $"return b.bookOrder as order, b.title, b.chapters as chapterNum, b.book order by b.bookOrder;";
        return await QueryAndReturnJsonAsync(query);
    }



    #endregion

    #region book

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
    public async Task<IActionResult> getScriptures(int pageNumber, int pageSize, string scripture = "All")
    {
        string where = string.Empty;
        if (!scripture.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            where = $"WHERE b.Label = '{scripture}";
        }

        var query =
            @"
            MATCH (b:Book) return b 
            <skip> 
            <limit>;
            ";

        var totalCount = 1;
        var totalPages = 1;

        var skip = (pageNumber - 1) * pageSize;
        var limit = 1;

        query = query.Replace("<skip>",  $"SKIP  {skip}");
        query = query.Replace("<limit>", $"LIMIT {pageSize}");

        var jsonResult = await _grapthRepository.RunQueryAsync(query);
        return Content(jsonResult, "application/json");

        Response.AddPaginationHeader(new PaginationHeader(1, 11, 333, 33));


        return await QueryAndReturnJsonAsync(query);
    }



    [HttpGet("{translation}/books")]
    public async Task<IActionResult> getBooksByTranslation(string translation, string testament = "All")
    {
        string where = string.Empty;
        string orderBy = "n.testament desc, ";

        if (!testament.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            where = $"WHERE b.testament = '{testament}' ";
        }
        var query =
            $@"
        MATCH(t:Translation) where t.id = '{translation}' 
        MATCH(t)-[rt:CONTAINS]->(n:Book)
        {where}
        return n, t.id as translation order by {orderBy} n.bookOrder ";

        return await QueryAndReturnJsonAsync(query);
    }

    [HttpGet("books/{book}/chapters")]
    public async Task<IActionResult> getBookChapters(string book)
    {
        var query =
            $@"
            MATCH (n:Book) where n.book ='{book}'
            MATCH(n)-[r:CONTAINS]->(c:Chapter)
            MATCH(n)<--(t:Translation)
            RETURN n, t.id as translation, c.num as chapter order by c.num;
            ";
        return await QueryAndReturnJsonAsync(query);
    }


    [HttpGet("{translation}/books/{book}/chapters")]
    public async Task<IActionResult> getBookChapters(string translation, string book)
    {
        var query =
            $@"
            MATCH(t:Translation) where t.id = '{translation}' 
            MATCH(t)-->(n:Book) where n.book ='{book}'
            MATCH(n)-[r:CONTAINS]->(c:Chapter)
            RETURN n,n.chapters,t.id,   c.book, c.num order by c.num;            ";
        return await QueryAndReturnJsonAsync(query);
    }


    [HttpGet("bibles")]
    public async Task<IActionResult> getBibles(string translation)
    {
        var query =
            $@"
        MATCH (b:Book) return b;
        ";

        return await QueryAndReturnJsonAsync(query);
    }

    [HttpGet("nareks")]
    public async Task<IActionResult> getNareks(string translation)
    {
        var query =
            $@"
        MATCH (b:Book) return b;
        ";

        return await QueryAndReturnJsonAsync(query);
    }


    [HttpGet("books/{book}")]
    public async Task<IActionResult> getBook(string book)
    {
        var query =
            $"match (b:Book) where b.book = '{book}' return b;";
        return await QueryAndReturnJsonAsync(query);
    }

    [HttpGet("{translation}/books/{book}")]
    public async Task<IActionResult> getBook(string translation, string book)
    {
        var query = $@"MATCH(t:Translation) where t.id = '{translation}' 
                  MATCH(t)-[r:CONTAINS]->(b:Book) where b.book = '{book}'
                  return b;";
        return await QueryAndReturnJsonAsync(query);
    }

    #endregion

    #region neo4j helpers

    /// <summary>
    /// return array of json result
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    private async Task<IActionResult> QueryAndReturnJsonAsync(string query)
    {
        try
        {
            Console.WriteLine(query);
            var jsonResult = await _grapthRepository.RunQueryAsync(query);
            return Content(jsonResult, "application/json");
            //return Content( $"{JsonConvert.SerializeObject(jsonResult)}", "application/json");
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, $"Failed on when running query {query}");
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

}

#endregion
