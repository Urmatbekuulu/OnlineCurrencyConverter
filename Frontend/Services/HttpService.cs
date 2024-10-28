using SharedModels.AuthViewModels;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Frontend.Services;

public interface IHttpService
{
    public Task<HttpResponseMessage> HttpPostAsJsonAsync<T>(string url, T model);
    public Task<HttpResponseMessage> HttpGetAsync(string url);
    public Task<HttpResponseMessage> HttpPutAsJsonAsync<T>(string url, T model);
    public Task<HttpResponseMessage> HttpDeleteAsync(string url);




}

public class HttpService : IHttpService
{
    private readonly ILocalStorageService _localStorage;

    private readonly HttpClient _httpClient;
    private readonly CustomAuthenticationStateProvider _authenticationState;



    public HttpService(ILocalStorageService localStorage,HttpClient httpClient, CustomAuthenticationStateProvider authenticationState)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
        _authenticationState = authenticationState;
    }
    
    public async Task<HttpResponseMessage> HttpPostAsJsonAsync<T>(string url,T model)
    {
        var response = await _httpClient.PostAsJsonAsync(url,model);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
           await RefreshAccessToken();
           response = await _httpClient.PostAsJsonAsync(url,model);

        }
        return response;

    }
    
    public async Task<HttpResponseMessage> HttpPutAsJsonAsync<T>(string url,T model)
    {
        var response = await _httpClient.PutAsJsonAsync(url,model);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
           await RefreshAccessToken();
           response = await _httpClient.PutAsJsonAsync(url,model);


        }
 
        return response;

    }

    public async Task<HttpResponseMessage> HttpDeleteAsync(string url)
    {
        var response = await _httpClient.DeleteAsync(url);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
           await RefreshAccessToken();
           response = await _httpClient.DeleteAsync(url);


        }
   
        return response;
        
    }

    public async Task<HttpResponseMessage> HttpGetAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
           await RefreshAccessToken();
           response = await _httpClient.GetAsync(url);

        }
        
        return response;
    }

    private async Task RefreshAccessToken()
    {
        var refreshToken = await _localStorage.GetItem<string>("refreshToken");

        var refreshResponse =
            await _httpClient.PostAsJsonAsync<RefreshTokenViewModel>("api/refreshtoken",
                new() { RefreshToken = refreshToken });

        if (!refreshResponse.IsSuccessStatusCode)
        {
            await _authenticationState.LogoutAsync();
            
        }
        else
        {
            await _authenticationState.SaveJwtToken(refreshResponse);

            var token = await _localStorage.GetItem<string>("jwtToken");

            _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {token}");

        }
        
    }
}