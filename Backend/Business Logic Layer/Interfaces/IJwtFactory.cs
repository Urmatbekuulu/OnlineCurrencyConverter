using Backend.Data_Access_Layer;
using System.Security.Claims;

namespace Backend.Business_Logic_Layer
{
    public interface IJwtFactory
    {
        /// <summary>
        /// Creates Jwt token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="additionalClaims"></param>
        /// <returns></returns>
        Task<(string token, DateTime expiration)> CreateTokenAsync(string userId, string email,
            IEnumerable<Claim> additionalClaims = default);

        /// <summary>
        /// Creates RefreshToken
        /// </summary>
        /// <param name="ipAdress"></param>
        /// <returns></returns>
        RefreshToken CreateRefreshToken(string ipAdress);
    }
}
