using Backend.Data_Access_Layer;

namespace Backend.Data_Access_Layer
{
    public interface ICurrencyRepository
    {
        public Task<IEnumerable<Currency>> GetCurrenciesByActualDate(DateOnly actualDate);
        public Task<IEnumerable<Currency>> GetCurrenciesByCode(string currencyCode);
        public Task AddCurrency(Currency currency);
        public Task UpdateCurrencyExchangeRates(Currency currency);
    }
}