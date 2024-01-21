using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HolyGraph.API.Extensions;
using HolyGraph.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace HolyGraph.API.Controllers;

public class VersesController : BaseApiController
{
    private readonly INeo4jGraphRepository _grapthRepository;
    private readonly ILogger<VersesController> _logger;

    public VersesController(INeo4jGraphRepository grapthRepository, ILogger<VersesController> logger)
    {
        _grapthRepository = grapthRepository;     
        _logger = logger;           
    }

    /// Verses
    [HttpGet("{translation}/books/{book}/chapters/{chapter}/oneverse/{verseNum}/")]
    public async Task<IActionResult> getVerse(string translation, string book, int chapter, int verseNum)
    {
        var query = 
            $@"
            MATCH(t:Translation) where t.id = '{translation}' 
            MATCH(t)-[rt:CONTAINS]->(n:Book) where n.book = '{book}'
            MATCH (n)<-[r:CONTAINS]->(ch:Chapter)-[:CONTAINS]->(v:Verse) 
            where ch.num = {chapter} and v.verse = {verseNum}
            return v, t.id as translation, n.title as bookTitle order by v.verseOrder
            ";

        return await QueryAndReturnJsonAsync(query);
    }


    /// Verse range
    [HttpGet("{translation}/books/{book}/chapters/{chapter}/verses/{v1}/")]
    public async Task<IActionResult> getVerseRange(string translation, string book, int chapter, int v1, int v2)
    {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Request: Verse Range enter");
        Console.WriteLine($"URL: scripture/{translation}/books/{book}/chapters/{chapter}/verses/{v1}? to={v2}");

        var query = 
            $@"
            MATCH(t:Translation) where t.id = '{translation}' 
            MATCH(t)-[rt:CONTAINS]->(n:Book) where n.book = '{book}'
            MATCH (n)<-[r:CONTAINS]->(ch:Chapter)-[:CONTAINS]->(v:Verse) 
            where ch.num = {chapter} and v.verse >= {v1} and v.verse <= {v2}
            return v, t.id as translation, n.title as bookTitle order by v.verseOrder
            ";

        return await QueryAndReturnJsonAsync(query);
    }



    /// Chapter's  sections
    [HttpGet("{translation}/books/{book}/chapters/{chapter}/sections/")]
    public async Task<IActionResult> getChapterTitles(string translation, string book, int chapter)
    {
        var query = 
            $@"
            MATCH(t:Translation) where t.id = '{translation}' 
            MATCH(t)-[rt:CONTAINS]->(n:Book) where n.book = '{book}'
            MATCH (n)-[r:CONTAINS]-(ch:Chapter)-[r1:CONTAINS*]->(sec:Section)
            where ch.num = {chapter} 
            return sec order by ch.num, sec.num
            ";           

        return await QueryAndReturnJsonAsync(query);
    }


    /// <summary>
    /// Chapter's concret  sections
    /// </summary>
    /// <param name="translation"></param>
    /// <param name="book"></param>
    /// <param name="chapter"></param>
    /// <param name="secnum"></param>
    /// <returns>Sections</returns>
    /// <remarks>Chapter's concret  sections!</remarks>
    /// <response code="200">Success</response>
    /// <response code="500">Oops! Can't get data right now</response>
    [HttpGet("{translation}/books/{book}/chapters/{chapter}/sections/{secnum}")]
    public async Task<IActionResult> getChapterTitles(string translation, string book, int chapter, int secnum)
    {
        var query = 
            $@"
            MATCH(t:Translation) where t.id = '{translation}'
            MATCH(t)-[rt: CONTAINS]->(n: Book) where n.book = '{book}'
            MATCH(n)-[r:CONTAINS]-(ch:Chapter)-[r1:CONTAINS*]->(sec:Section)
            where ch.num = {chapter} and sec.num = {secnum}
            return sec order by ch.num, sec.num
            ";           

        return await QueryAndReturnJsonAsync(query);
    }


    /// Book sections
    [HttpGet("{translation}/books/{book}/sections")]
    public async Task<IActionResult> getBookSections(string translation, string book)
    {
        var query = 
            $@"
            MATCH(t:Translation) where t.id = '{translation}'
            MATCH(t)-[rt: CONTAINS]->(n: Book) where n.book = '{book}'
            MATCH(n)-[r:CONTAINS]-(ch:Chapter)-[r1:CONTAINS*]->(sec:Section)
            return sec order by ch.num, sec.num
            ";           

        return await QueryAndReturnJsonAsync(query);
    }

    /*  [HttpGet("{language}/books/{book}/chapters/{chapter}/sections/{section}")]
        public async Task<ContentResult> getChapterTitles(string language, string book, int chapter)
        {
            var query = $@"MATCH (n:Book:{book}:{language}) where n.book = '{book}'
            MATCH (n:Book:Matthew:Ashxarhabar)-[r:CONTAINS]-(ch:Chapter)-[r1:CONTAINS*]->(sec:Section)
            where ch.num = {chapter} 
            return ch.title, sec.title order by ch.num, sec.num";           

            Console.WriteLine(query);
            var str = await getQueryJson(query);
            return Content(str, "application/json");
        }
*/



    private async Task<IActionResult> QueryAndReturnJsonAsync(string query)
    {
        try
        {
            Console.WriteLine(query);
            var jsonResult = await _grapthRepository.RunQueryAsync(query);
            return Content(jsonResult, "application/json");
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, @"Failed on when running query {query}");
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}