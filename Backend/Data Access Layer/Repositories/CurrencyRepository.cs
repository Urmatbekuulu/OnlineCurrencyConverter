using Backend.Data_Access_Layer;

namespace Backend.Data_Access_Layer
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationDbContext _context;
        public CurrencyRepository(ApplicationDbContext context) {
            _context = context;
        }
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
