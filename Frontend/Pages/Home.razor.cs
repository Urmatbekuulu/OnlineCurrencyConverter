using Frontend.Services;
using Microsoft.AspNetCore.Components;
using SharedModels.CurrenciesViewModel;
using System.Net.Http.Json;

namespace Frontend.Pages
{
    public partial class Home
    {
        [Inject] private IHttpService HttpService { get; set; }

        private List<CurrencyViewModel> Currencies = new();
        private CurrencyViewModel newItem = new CurrencyViewModel() { ActualDate = DateOnly.FromDateTime(DateTime.Now)};
        private CurrencyViewModel editItem = null;
        private async Task AddItem()
        {
            var response = await HttpService.HttpPostAsJsonAsync("api/currencies/create", newItem);
            if (!response.IsSuccessStatusCode) return;
        }
        private async Task UpdateItem()
        {
            var response = await HttpService.HttpPostAsJsonAsync("api/currencies/update", editItem);
            if (!response.IsSuccessStatusCode) return;
        }
        private void StartEdit(CurrencyViewModel item)
        {
            editItem = new CurrencyViewModel
            {
               ActualDate = item.ActualDate,
               CurrencyCode = item.CurrencyCode,
               BuyRateToBaseCurrency = item.BuyRateToBaseCurrency,
               SellRateToBaseCurrency = item.SellRateToBaseCurrency,
               CurrencyName = item.CurrencyName
            };
        }
        protected override async Task OnInitializedAsync()
        {
            var result = await AuthProvider.GetAuthenticationStateAsync();
            if (result.User?.Identity?.IsAuthenticated == true)
            {
                Currencies = await GetAllCurrencies();

            }
            else
            {
                Currencies = await GetTodaysCurrencies();
            }
        }
        public async Task<List<CurrencyViewModel>> GetTodaysCurrencies()
        {
            var response = await HttpService.HttpGetAsync($"api/currencies");
            var result = await response.Content.ReadFromJsonAsync<List<CurrencyViewModel>>();
            if (result is null) return default;
            return result;
        }
        public async Task<List<CurrencyViewModel>> GetAllCurrencies()
        {
            var response = await HttpService.HttpGetAsync($"api/all/currencies");
            var result = await response.Content.ReadFromJsonAsync<List<CurrencyViewModel>>();
            if (result is null) return default;
            return result;
        }
    }
}
