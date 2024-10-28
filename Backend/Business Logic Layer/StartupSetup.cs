using Backend.Data_Access_Layer;
using Microsoft.EntityFrameworkCore;

namespace Backend.Business_Logic_Layer
{
    public static class StartupSetup
    {
        public static void AddBusinessAccessLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtIdentity(configuration.GetSection(nameof(JwtConfiguration)));
        }
    }
}
