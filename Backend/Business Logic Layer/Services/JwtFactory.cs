﻿using Backend.Data_Access_Layer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Backend.Business_Logic_Layer
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtConfiguration _jwtOptions;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtFactory(IOptions<JwtConfiguration> jwtOptions, UserManager<ApplicationUser> userManager)
        {
            this._jwtOptions = jwtOptions.Value;
            this._userManager = userManager;
            ThrowIfInvalidOptions(_jwtOptions);

        }

        private static void ThrowIfInvalidOptions(JwtConfiguration options)
        {
            if (options == null)
            {
                throw new ArgumentNullException();
            }

            if (options.ValidFor <= TimeSpan.Zero || options.RefreshTokenTimeSpan <= TimeSpan.Zero)
            {
                throw new ArgumentException("TimeSpan must be bigger than 0");
            }
            if (options.SigningCredentials == null)
            {
                throw new ArgumentException(nameof(JwtConfiguration.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentException(nameof(JwtConfiguration.JtiGenerator));
            }

        }

        public async Task<(string token, DateTime expiration)> CreateTokenAsync(string userId, string email, IEnumerable<Claim> additionalClaims = default)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.Name, email),
            new(JwtRegisteredClaimNames.Jti, _jwtOptions.JtiGenerator()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(_jwtOptions.IssuedAt).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64)
        };

            claims.AddRange(additionalClaims ?? Array.Empty<Claim>());

            var expires = _jwtOptions.Expiration;

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: expires,
                signingCredentials: _jwtOptions.SigningCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            return (tokenHandler.WriteToken(jwt), expires);
        }

        public RefreshToken CreateRefreshToken(string ipAddress)
        {

            return new RefreshToken
            {
                Token = RandomToken(),
                CreatedByIp = ipAddress,
                CreatedDateTime = DateTime.UtcNow,
                ExpireDateTime = DateTime.UtcNow.Add(_jwtOptions.RefreshTokenTimeSpan),

            };
        }

        private static string RandomToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
