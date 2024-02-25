using HolyGraph.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HolyGraph.API.Controllers;

public class TranslationsController : BaseApiController
{
    private readonly ITranslationRepository _translationRepository;

    public TranslationsController(ITranslationRepository translationRepository)
    {
        _translationRepository = translationRepository;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<TranslationDto>>> getTranslations()
    {
        var ts = await _translationRepository.GetTranslationsAsync();
        return Ok(ts);
    }

    [HttpGet("{translation}/books")]
    public async Task<ActionResult<IEnumerable<BookDto>>> getTranslationBooks(string translation)
    {
        var ts = await _translationRepository.GetTranslationBooksAsync(translation);
        return Ok(ts);
    }

}
