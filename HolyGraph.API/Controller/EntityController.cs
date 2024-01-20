using HolyGraph.API.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;

namespace HolyGraph.API.Controllers;

public class EntityController : BaseApiController
{
    private readonly INeo4jGraphRepository _grapthRepository;
    private readonly ILogger<EntityController> _logger;

    public EntityController(INeo4jGraphRepository grapthRepository, ILogger<EntityController> logger)
    {
        _grapthRepository = grapthRepository;     
        _logger = logger;           
    }



    [HttpGet("places")]
    public async Task<IActionResult> getPlaces()
    {
        var query =
            @"
            MATCH (p:Place) return p limit 50;
            ";
        return await QueryAndReturnJsonAsync(query);
    }

    [HttpGet("persons")]
    public async Task<IActionResult> getPersons()
    {
        var query =
            @"
            MATCH (p:Person) return p limit 50;
            ";
        return await QueryAndReturnJsonAsync(query);
    }

    [HttpGet("events")]
    public async Task<IActionResult> getEvents()
    {
        var query =
            @"
            MATCH (e:Event) return e;
            ";
        return await QueryAndReturnJsonAsync(query);
    }

    [HttpGet("eastons")]
    public async Task<IActionResult> getEastons()
    {
        var query =
            @"
            MATCH (n:`Easton's`) RETURN n limit 100;
            ";
        return await QueryAndReturnJsonAsync(query);
    }

    [HttpGet("divisions")]
    public async Task<IActionResult> getDivisions()
    {
        var query =
            @"
            MATCH (n:Division) RETURN n;
            ";
        return await QueryAndReturnJsonAsync(query);
    }

    [HttpGet("translations")]
    public async Task<IActionResult> getTranslatins()
    {
        var query =
            @"
            MATCH (n:Translation) RETURN n;
            ";
        return await QueryAndReturnJsonAsync(query);
    }

    [HttpGet("scriptures")]
    public async Task<IActionResult> getScriptures()
    {
        var query =
            @"
            MATCH (n:Scripture) RETURN n;
            ";
        return await QueryAndReturnJsonAsync(query);
    }

    [HttpGet("commentaries")]
    public async Task<IActionResult> getCommentary()
    {
        var query =
            @"
            MATCH (n:Commentary) RETURN n;
            ";
        return await QueryAndReturnJsonAsync(query);
    }

    [HttpGet("years")]
    public async Task<IActionResult> getYears()
    {
        var query =
            @"
            MATCH (n:Year) RETURN n;
            ";
        return await QueryAndReturnJsonAsync(query);
    }

    [HttpGet("armats")]
    public async Task<IActionResult> getArmats()
    {
        var query =
            @"
            MATCH (n:Armat) RETURN n limit 100;
            ";
        return await QueryAndReturnJsonAsync(query);
    }


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
            _logger.LogError(ex, @"Failed on when running query {query}");
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

}
