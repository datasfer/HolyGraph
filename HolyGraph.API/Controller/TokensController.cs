using Microsoft.AspNetCore.Mvc;

namespace HolyGraph.API.Controllers;

public class TokensController : BaseApiController
{

    private readonly ITokenRepository _tokenRepository;
    public TokensController(ITokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }

    [HttpGet("armatId")]
    public async Task<ActionResult<IEnumerable<ArmatDto>>> getTokensByArmatId(int armatId)
    {
        var ts = await _tokenRepository.GetArmatTokensAsync(armatId);
        return Ok(ts);
    }

    //[HttpGet("{armat}")]
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<ArmatDto>>> getTokensByArmat(string armat)
    {
        var ts = await _tokenRepository.GetArmatTokensAsync(armat);
        return Ok(ts);
    }
}
