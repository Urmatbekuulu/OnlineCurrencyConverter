using Backend.Business_Logic_Layer.Interfaces;
using Backend.Data_Access_Layer.Entity;

namespace Backend.Business_Logic_Layer.Services
{
    public class CurrencyService : ICurrencyService
    {
        public Task AddCurrency(Currency currency)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Currency>> GetCurrenciesByActualDate(DateOnly actualDate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Currency>> GetCurrenciesByCode(string currencyCode)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCurrencyExchangeRates(Currency currency)
        {
            throw new NotImplementedException();
        }
    }
}
