using LeilAFinafrica.Domain.Configuration;

namespace LeilAFinafrica.WepAPI.Confogurations
{
    public static class CorsConfig
    {
        public const string DEFAULT_POLICY = "DEFAULT_POLICY";
        public const string POLICY_ALL = "POLICY_ALL";
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            // Récupère la section Cors de la configuration et la lie à l'objet CorsOption.
            CorsOption corsOption = new CorsOption();
            configuration.GetSection("Cors").Bind(corsOption);

            services.AddCors(options =>
            {
                // Configure une politique CORS qui permet à n'importe quel domaine d'accéder à l'API avec n'importe quelle méthode HTTP.
                options.AddPolicy(POLICY_ALL, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });

                // Configure une politique CORS avec des origines spécifiques autorisées à accéder à l'API.
                options.AddPolicy(DEFAULT_POLICY, builder =>
                {
                    builder.WithOrigins(corsOption.Origin)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
    }
}
