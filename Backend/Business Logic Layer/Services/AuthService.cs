using Backend.Data_Access_Layer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedModels.AuthViewModels;

namespace Backend.Business_Logic_Layer
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly string _ipAddress;
        private readonly ApplicationDbContext _dbContext;

        public AuthService(SignInManager<ApplicationUser> signInManager, IJwtFactory jwtFactory, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext) {
            
            _jwtFactory = jwtFactory;
            _signInManager = signInManager;
            var httpContext = httpContextAccessor.HttpContext;
            _dbContext = dbContext;

            _ipAddress = httpContext != null && httpContext.Request.Headers.ContainsKey("X-Forwarded-For")
                ? httpContext.Request.Headers["X-Forwarded-For"]
                : httpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
        public async Task<SignInResponse> RefreshTokenAsync(RefreshTokenViewModel request)
        {
            var response = new SignInResponse();
            response.IsSuccess = false;
            response.Errors = new List<string>();

            var refreshToken = await _dbContext.RefreshTokens
           .Include(x => x.User)
           .FirstOrDefaultAsync(x => x.Token == request.RefreshToken);

            if (refreshToken == null || !refreshToken.IsActive || refreshToken.User == null)
                response.Errors.Add("Error token already revoked or unavailable");

            var (jwtToken, expires) = await _jwtFactory.CreateTokenAsync(refreshToken.UserId, refreshToken.User.Email);

            response.IsSuccess = true;
            response.Username = refreshToken.User.UserName;
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            response.JwtExpires = expires;
            return response;
        }

        public async Task<SignInResponse> SignInAsync(SignInRequest request)
        {
            var response = new SignInResponse();
            response.IsSuccess = false;
            response.Errors = new List<string>();
            var user = await _signInManager.UserManager.FindByNameAsync(request.Username);

            if (user == null) 
                response.Errors.Add("User is not found");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
                response.Errors.Add("User is not found");

            var (jwtToken, expires) = await _jwtFactory.CreateTokenAsync(user.Id, user.Email);

            var refreshToken = _jwtFactory.CreateRefreshToken(_ipAddress);

            refreshToken.UserId = user.Id;

            _dbContext.RefreshTokens.Add(refreshToken);
            await _dbContext.SaveChangesAsync();

            response.IsSuccess = true;
            response.Username = user.UserName;
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            response.JwtExpires = expires;
            return response;
        }

        public async Task<bool> TryRevokeTokenAsync(RefreshTokenViewModel token)
        {
            var refreshToken = await _dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token.RefreshToken);

            if (refreshToken == null)
                return false;

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = _ipAddress;

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
