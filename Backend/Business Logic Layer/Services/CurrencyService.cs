using Backend.Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
using SharedModels.CurrenciesViewModel;

namespace Backend.Business_Logic_Layer;

public class CurrencyService : ICurrencyService
{
    private readonly ApplicationDbContext _context;

    public CurrencyService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddCurrencyAsync(CurrencyViewModel currency)
    {
        _context.Currencies.Add(new Currency()  { 
            CurrencyCode = currency.CurrencyCode, 
            CurrencyName = currency.CurrencyName,
            BuyRateToBaseCurrency = currency.BuyRateToBaseCurrency,
            SellRateToBaseCurrency = currency.SellRateToBaseCurrency,
            ActualDate = currency.ActualDate
        });

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CurrencyViewModel>> GetCurrenciesByActualDateAsync(DateOnly actualDate)
    {
       return await _context.Currencies
            .Where(c=>actualDate==c.ActualDate)
            .Select(c => new CurrencyViewModel() {
                CurrencyCode = c.CurrencyCode,
                CurrencyName = c.CurrencyName,
                BuyRateToBaseCurrency = c.BuyRateToBaseCurrency,
                SellRateToBaseCurrency = c.SellRateToBaseCurrency,
                ActualDate = c.ActualDate
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<CurrencyViewModel>> GetCurrenciesByCodeAsync(string currencyCode)
    {
        return await _context.Currencies
            .Where(c => currencyCode == c.CurrencyCode)
            .Select(c => new CurrencyViewModel()
            {
                CurrencyCode = c.CurrencyCode,
                CurrencyName = c.CurrencyName,
                BuyRateToBaseCurrency = c.BuyRateToBaseCurrency,
                SellRateToBaseCurrency = c.SellRateToBaseCurrency,
                ActualDate = c.ActualDate
            })
            .ToListAsync();
    }

    public async Task UpdateCurrencyExchangeRatesAsync(CurrencyViewModel currency)
    {
       var actualCurrency = await _context.Currencies.FindAsync(currency.CurrencyCode, currency.ActualDate);
        if (actualCurrency != null) {
            actualCurrency.BuyRateToBaseCurrency = currency.BuyRateToBaseCurrency;
            actualCurrency.SellRateToBaseCurrency = currency.SellRateToBaseCurrency;
            actualCurrency.CurrencyName = currency.CurrencyName;
        }
        await _context.SaveChangesAsync();
    }
}
