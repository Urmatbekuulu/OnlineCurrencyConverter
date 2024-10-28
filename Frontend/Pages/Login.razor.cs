using SharedModels.AuthViewModels;

namespace Frontend.Pages;

public partial class Login
{
    private readonly SignInRequest _model = new SignInRequest();
    private bool _loading;
    private string _error;

    protected override async void OnInitialized()
    {

        var result = await AuthProvider.GetAuthenticationStateAsync();
        if (result.User?.Identity?.IsAuthenticated == true) NavigationManager.NavigateTo("");

    }

    private async Task HandleValidSubmit()
    {
        _loading = true;
        try
        {
            var result = await AuthenticationStateProvider.LoginAsync(_model.Username, _model.Password);

        }
        catch (Exception ex)
        {
            _error = ex.Message;
        }

        _loading = false;
        StateHasChanged();
    }
}