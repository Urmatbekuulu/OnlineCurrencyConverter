using Backend.Data_Access_Layer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data_Access_Layer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity => entity.ToTable("Users"));
            builder.Entity<IdentityRole>(entity => entity.ToTable("Roles"));
            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogins"));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("RoleClaims"));
            builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserTokens"));

            builder.Entity<Currency>().HasData(
                new Currency()
                {
                    CurrencyCode = "USD",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)),
                    BuyRateToBaseCurrency = 85.6m,
                    SellRateToBaseCurrency = 85.7m,
                },
                new Currency()
                {
                    CurrencyCode = "USD",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-9)),
                    BuyRateToBaseCurrency = 85.6m,
                    SellRateToBaseCurrency = 85.7m,
                },
                new Currency()
                {
                    CurrencyCode = "USD",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-8)),
                    BuyRateToBaseCurrency = 85.6m,
                    SellRateToBaseCurrency = 85.7m,
                },
                new Currency()
                {
                    CurrencyCode = "USD",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)),
                    BuyRateToBaseCurrency = 85.6m,
                    SellRateToBaseCurrency = 85.7m,
                },
                new Currency()
                {
                    CurrencyCode = "USD",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-6)),
                    BuyRateToBaseCurrency = 85.6m,
                    SellRateToBaseCurrency = 85.7m,
                },
                new Currency()
                {
                    CurrencyCode = "USD",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)),
                    BuyRateToBaseCurrency = 85.6m,
                    SellRateToBaseCurrency = 85.7m,
                },
                new Currency()
                {
                    CurrencyCode = "USD",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-4)),
                    BuyRateToBaseCurrency = 85.6m,
                    SellRateToBaseCurrency = 85.7m,
                },
                new Currency()
                {
                    CurrencyCode = "USD",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)),
                    BuyRateToBaseCurrency = 85.6m,
                    SellRateToBaseCurrency = 85.7m,
                },
                new Currency()
                {
                    CurrencyCode = "USD",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)),
                    BuyRateToBaseCurrency = 85.6m,
                    SellRateToBaseCurrency = 85.7m,
                },
                new Currency()
                {
                    CurrencyCode = "USD",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                    BuyRateToBaseCurrency = 85.6m,
                    SellRateToBaseCurrency = 85.7m,
                },
                new Currency()
                {
                    CurrencyCode = "USD",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now),
                    BuyRateToBaseCurrency = 85.6m,
                    SellRateToBaseCurrency = 85.7m,
                },
                new Currency()
                {
                    CurrencyCode = "USD",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    BuyRateToBaseCurrency = 85.6m,
                    SellRateToBaseCurrency = 85.7m,
                },
                new Currency()
                {
                    CurrencyCode = "RUB",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)),
                    BuyRateToBaseCurrency = 0.85m,
                    SellRateToBaseCurrency = 0.87m,
                },
                new Currency()
                {
                    CurrencyCode = "RUB",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-9)),
                    BuyRateToBaseCurrency = 0.85m,
                    SellRateToBaseCurrency = 0.87m,
                },
                new Currency()
                {
                    CurrencyCode = "RUB",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-8)),
                    BuyRateToBaseCurrency = 0.85m,
                    SellRateToBaseCurrency = 0.87m,
                },
                new Currency()
                {
                    CurrencyCode = "RUB",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-7)),
                    BuyRateToBaseCurrency = 0.85m,
                    SellRateToBaseCurrency = 0.87m,
                },
                new Currency()
                {
                    CurrencyCode = "RUB",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-6)),
                    BuyRateToBaseCurrency = 0.85m,
                    SellRateToBaseCurrency = 0.87m,
                },
                new Currency()
                {
                    CurrencyCode = "RUB",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)),
                    BuyRateToBaseCurrency = 0.85m,
                    SellRateToBaseCurrency = 0.87m,
                },
                new Currency()
                {
                    CurrencyCode = "RUB",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-4)),
                    BuyRateToBaseCurrency = 0.85m,
                    SellRateToBaseCurrency = 0.87m,
                },
                new Currency()
                {
                    CurrencyCode = "RUB",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)),
                    BuyRateToBaseCurrency = 0.85m,
                    SellRateToBaseCurrency = 0.87m,
                },
                new Currency()
                {
                    CurrencyCode = "RUB",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)),
                    BuyRateToBaseCurrency = 0.85m,
                    SellRateToBaseCurrency = 0.87m,
                },
                new Currency()
                {
                    CurrencyCode = "RUB",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                    BuyRateToBaseCurrency = 0.85m,
                    SellRateToBaseCurrency = 0.87m,
                },
                new Currency()
                {
                    CurrencyCode = "RUB",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now),
                    BuyRateToBaseCurrency = 0.85m,
                    SellRateToBaseCurrency = 0.87m,
                },
                new Currency()
                {
                    CurrencyCode = "RUB",
                    ActualDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    BuyRateToBaseCurrency = 0.85m,
                    SellRateToBaseCurrency = 0.87m,
                }


            );
        }
    }
}
