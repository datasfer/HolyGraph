using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HolyGraph.API.Extensions;
using HolyGraph.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace HolyGraph.API.Controllers;

public class ScriptureController : BaseApiController
{
    private readonly INeo4jGraphRepository _grapthRepository;
    private readonly IDriver _driver;

    public ScriptureController(INeo4jGraphRepository grapthRepository)
    {
        _grapthRepository = grapthRepository;
        //_driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "neo"));
        _driver = GraphDatabase.Driver("bolt://195.201.22.161:7687", AuthTokens.Basic("neo4j", "neo"));
    }

    #region Meta information

    /// <summary>
    /// verse number of chapter of the book 
    /// </summary>
    /// <param name="translation"></param>
    /// <param name="girq"></param>
    /// <param name="ch_num"></param>
    /// <returns></returns>
    [HttpGet("book/verses/count")]
    public async Task<IActionResult> getBookChapterVerses(string translation, string book, int ch_num)
    {
        var query =
        $"MATCH(t:Translation) where t.id = '{translation}' " +
        $"match (b:Book {{book: '{book}' }})-[:CONTAINS]-()-[*]->(v:Verse) " +
        $"WHERE v.chapter={ch_num} " +
        $"return Max(v.verse) as num";

        return await QueryAndReturnJsonAsync(query);
    }

    /// <summary>
    /// chapter number of the book
    /// </summary>
    /// <param name="book"></param>
    /// <returns></returns>
    [HttpGet("book/chapters/count")]
    public async Task<IActionResult> getBookChaptersCount(string translation, string book)
    {
        var query =
            $"MATCH(t:Translation) where t.id = '{translation}' " +
            $"match (b:Book {{book: '{book}' }})-->(v:Chapter) " +
            $"return b.bookOrder as order, b.title, b.chapters as chapterNum, b.book;";
        return await QueryAndReturnJsonAsync(query);
    }

    #endregion

    #region book
    // curl -X GET "http://195.201.22.161:33010/Scripture/books" -H "accept: */*"
    // curl -X GET "http://localhost:33010/Scripture/books" -H "accept: */*"
    [HttpGet("scriptures")]
    public async Task<IActionResult> getScriptures(string scripture = "All")
    {
        string where = string.Empty;
        if (!scripture.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            where = $"WHERE b.Label = '{scripture}";
        }

        var query =
            @"
        MATCH (b:Book) return b;
        ";
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

    [HttpGet("translations/{translation}/books/{book}")]
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
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

}

#endregion
