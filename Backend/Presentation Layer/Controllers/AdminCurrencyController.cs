using Backend.Business_Logic_Layer;
using Backend.Data_Access_Layer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedModels.CurrenciesViewModel;

namespace Backend.Presentation_Layer;

public class AdminController : ControllerBase
{
    private readonly ICurrencyService _currencyService;
    private readonly ApplicationDbContext _context;

    public AdminController(ICurrencyService currencyService, ApplicationDbContext context)
    {
        _currencyService = currencyService;
        _context = context;
    }
    [HttpGet("/api/all/currencies")]
    public async Task<ActionResult<IEnumerable<CurrencyViewModel>>> GetAllCurrencies()
    {
        return Ok(await _context.Currencies.ToListAsync());
    }
    [HttpPost("/api/currencies/create")]
    public async Task<IActionResult> AddCurrency([FromBody] CurrencyViewModel newCurrency)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _currencyService.AddCurrencyAsync(newCurrency);
        return Ok();
    }
    [HttpPost("/api/currencies/update")]
    public async Task<IActionResult> UpdateCurrency([FromBody] CurrencyViewModel newCurrency)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _currencyService.UpdateCurrencyExchangeRatesAsync(newCurrency);
        return Ok();
    }
}
