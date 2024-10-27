using SharedModels.AuthViewModels;

namespace Backend.Business_Logic_Layer
{
    public class AuthService : IAuthService
    {
        public Task<SignInResponse> RefreshTokenAsync(RefreshTokenViewModel request)
        {
            throw new NotImplementedException();
        }

        public Task<SignInResponse> SignInAsync(SignInRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryRevokeTokenAsync(RefreshTokenViewModel refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
