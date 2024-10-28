using System.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Frontend.Services;
using SharedModels.AuthViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

public sealed class CustomAuthenticationStateProvider : AuthenticationStateProvider
{


    private static readonly ClaimsPrincipal Anonymous = new();
    
    private readonly HttpClient _httpClient;

    private readonly ILocalStorageService _localStorage;

    private ClaimsPrincipal _cachedPrincipal;

    public CustomAuthenticationStateProvider(HttpClient httpClient,ILocalStorageService localStorageService)
    {
        _localStorage = localStorageService;
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
       // return new AuthenticationState(Admin);
        if (_cachedPrincipal is not null)
        {
            return new AuthenticationState(_cachedPrincipal);
        }

        var principal =await LoadAccountAsync();
        if (principal is not null)
        {
            _cachedPrincipal = principal;
            return new AuthenticationState(principal);
            
        }
        
        return new AuthenticationState(Anonymous);
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var response =  await _httpClient.PostAsJsonAsync("api/signin",new
        {
            Username = username,
            Password = password
        });

        await SaveJwtToken(response);
        
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        
        return true;

    }

    public async Task<bool> SaveJwtToken(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<SignInResponse>();

        if (result == null) return false;
        
        await Task.WhenAll(
            _localStorage.SetItem("jwtToken",result.JwtToken),
            _localStorage.SetItem("jwtExpires", result.JwtExpires),
            _localStorage.SetItem("username", result.Username),
            _localStorage.SetItem("refreshToken", result.RefreshToken)
            );
        
        var token = await _localStorage.GetItem<string>("jwtToken");
        
        _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {token}");

        return true;
    }

    public async Task LogoutAsync()
    {
        _cachedPrincipal = default;

        var refreshToken = await _localStorage.GetItem<string>("refreshToken");
        
        await _httpClient.PostAsJsonAsync<RefreshTokenViewModel>("api/revoketoken"
            ,new(){RefreshToken = refreshToken });
        
        await Task.WhenAll(_localStorage.RemoveItem("jwtToken"),
            _localStorage.RemoveItem("username"),
            _localStorage.RemoveItem("jwtExpires"),
            _localStorage.RemoveItem("refreshToken"));
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(Anonymous)));
    }
    
    private async Task<ClaimsPrincipal> LoadAccountAsync()
    {
        var jwtToken =await _localStorage.GetItem<string>("jwtToken");
        if (String.IsNullOrEmpty(jwtToken))
        {
            return null;
        }
        
        var identity = new ClaimsIdentity();
        var tokenHandler = new JwtSecurityTokenHandler();
        if (tokenHandler.CanReadToken(jwtToken))
        {
            var jwtSecurityToken = tokenHandler.ReadJwtToken(jwtToken);
            
            identity = new ClaimsIdentity(jwtSecurityToken.Claims,"bearer");
            
        }
        
        var principal = new ClaimsPrincipal(identity);
        return principal;
    }

   
}
