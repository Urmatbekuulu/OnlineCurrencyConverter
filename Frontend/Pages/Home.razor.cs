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

        protected override async Task OnInitializedAsync()
        {
            Currencies = await GetTodaysCurrencies();
        }
        public async Task<List<CurrencyViewModel>> GetTodaysCurrencies()
        {
            var response = await HttpService.HttpGetAsync($"api/currencies");
            var result = await response.Content.ReadFromJsonAsync<List<CurrencyViewModel>>();
            if (result is null) return default;
            return result;
        }
    }
}
