using Backend.Business_Logic_Layer;
using Microsoft.AspNetCore.Mvc;
using SharedModels.CurrenciesViewModel;

namespace Backend.Presentation_Layer;

public class PublicCurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    public PublicCurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }
    [HttpGet("/api/currencies/{day:datetime?}")]
    public async Task<ActionResult<IEnumerable<CurrencyViewModel>>> GetCurrencyByDate([FromQuery] DateOnly? day)
    {
       return Ok(await _currencyService.GetCurrenciesByActualDateAsync(day ?? DateOnly.FromDateTime(DateTime.Now)));
    }
}
