using Backend.Data_Access_Layer.Entity;

namespace Backend.Business_Logic_Layer.Interfaces
{
    public interface ICurrencyService
    {
        public Task<IEnumerable<Currency>> GetCurrenciesByActualDate(DateOnly actualDate);
        public Task<IEnumerable<Currency>> GetCurrenciesByCode(string currencyCode);
        public Task AddCurrency(Currency currency);
        public Task UpdateCurrencyExchangeRates(Currency currency);
    }
}
