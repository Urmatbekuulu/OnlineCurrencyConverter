using SharedModels.AuthViewModels;

namespace Backend.Business_Logic_Layer
{
    public interface IAuthService
    {
        public Task<SignInResponse> SignInAsync(SignInRequest request);
        public Task<SignInResponse> RefreshTokenAsync(RefreshTokenViewModel request);
        public Task<bool> TryRevokeTokenAsync(RefreshTokenViewModel refreshToken);

    }
}
