using Microsoft.EntityFrameworkCore;

namespace Backend.Data_Access_Layer
{
    public static class StartupSetup
    {
        public static void AddDbContexts(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
        }

        public static void AddDataAccessLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

        }
    }
}
