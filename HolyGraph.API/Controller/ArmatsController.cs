using HolyGraph.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HolyGraph.API.Controllers;

public class ArmatsController: BaseApiController
{
    private readonly IArmatRepository _armatRepository;
    public ArmatsController(IArmatRepository armatRepository)
    {
        _armatRepository = armatRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArmatDto>>> getArmats([FromQuery] PaginationParams paginationParams)
    {
        var bs = await _armatRepository.GetArmatsAsync(paginationParams);

        Response.AddPaginationHeader(new PaginationHeader(bs.CurrentPage, bs.PageSize, bs.TotalCount, bs.TotalPages));

        return Ok(bs);
    }

    [HttpGet("{startsWith}")]
    public async Task<ActionResult<IEnumerable<ArmatDto>>> getArmatsbyStartLetter(string startsWith, bool ignoreCase = false)
    {
        var armats = await _armatRepository.GetArmatsByStartingLetterAsync(startsWith, ignoreCase);
        return Ok(armats);
    }
}
