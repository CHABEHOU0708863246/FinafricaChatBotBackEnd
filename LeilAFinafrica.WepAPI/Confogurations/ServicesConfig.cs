using LeilAFinafrica.DataAccessLayer.DbContext;
using LeilAFinafrica.Services;
using LeilAFinafrica.Services.VehicleQuoteServices;
using MongoDB.Driver;

namespace LeilAFinafrica.WepAPI.Confogurations
{
    public static class ServicesConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Infrastructure Services
            services.AddSingleton<InsurancePackService>();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            #endregion

            #region MongoDB Configuration
            var mongoDbSettings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            services.AddSingleton<IMongoClient>(new MongoClient(mongoDbSettings.ConnectionString));
            services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(mongoDbSettings.DatabaseName);
            });
            #endregion

            #region Business Services
            services.AddHttpClient<IVehicleQuoteService, VehicleQuoteService>();
            #endregion
        }
    }
}
