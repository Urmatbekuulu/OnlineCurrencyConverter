using Backend.Data_Access_Layer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Backend.Business_Logic_Layer;

public static class JwtDependenciesConfiguration
{
    public static void AddJwtIdentity(this IServiceCollection services, IConfigurationSection jwtConfiguration)
    {
        services.AddScoped<IJwtFactory, JwtFactory>();
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var signingKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(jwtConfiguration["Secret"]));
        var signinCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var issuer = jwtConfiguration[nameof(JwtConfiguration.Issuer)];
        var audience = jwtConfiguration[nameof(JwtConfiguration.Audience)];
        var validFor = TimeSpan.FromMinutes(Convert.ToInt32(jwtConfiguration[nameof(JwtConfiguration.ValidFor)]));
        TimeSpan refreshTokenTimeSpan = new();

        TimeSpan.TryParse(jwtConfiguration[nameof(JwtConfiguration.RefreshTokenTimeSpan)],
                out refreshTokenTimeSpan);


        JwtSecurityTokenHandler.DefaultInboundClaimFilter.Clear();

        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,

            ValidateAudience = true,
            ValidAudience = audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signinCredentials.Key,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero

        };

        services.Configure<JwtConfiguration>(options =>
        {
            options.Issuer = issuer;
            options.Audience = audience;
            options.ValidFor = validFor;
            options.SigningCredentials = signinCredentials;
            options.RefreshTokenTimeSpan = refreshTokenTimeSpan;

        });

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = issuer;
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
                configureOptions.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.HttpContext.Request.Headers["Authorization"];
                        if (token.Count > 0 && token[0].StartsWith("Token", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Token = token[0]["Token ".Length..].Trim();
                        }
                        return Task.CompletedTask;

                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Add("Token-Expired", "true");

                        return Task.CompletedTask;
                    }

                };

            });

    }
}
