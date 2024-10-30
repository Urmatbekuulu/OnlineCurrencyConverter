using Backend.Data_Access_Layer;
using SharedModels.CurrenciesViewModel;

namespace Backend.Business_Logic_Layer
{
    public interface ICurrencyService
    {
        public Task<IEnumerable<CurrencyViewModel>> GetCurrenciesByActualDateAsync(DateOnly actualDate);
        public Task<IEnumerable<CurrencyViewModel>> GetCurrenciesByCodeAsync(string currencyCode);
        public Task AddCurrencyAsync(CurrencyViewModel currency);
        public Task UpdateCurrencyExchangeRatesAsync(CurrencyViewModel currency);
    }
}
