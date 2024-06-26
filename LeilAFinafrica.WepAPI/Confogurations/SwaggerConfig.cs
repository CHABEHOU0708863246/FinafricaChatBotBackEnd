using Microsoft.OpenApi.Models;
using System.Reflection;

namespace LeilAFinafrica.WepAPI.Confogurations
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ChatBot Leila de Finafrica Project Web API pour la soutenance",
                    Description = "<h2>API du chatbot de l'application web OAV Finafrica</h2>",
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Input the JWT like: Bearer {your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    }
                };
                s.AddSecurityDefinition("Bearer", securitySchema);
                s.AddSecurityRequirement(new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                foreach (var xmlFilePath in Directory.EnumerateFiles(AppContext.BaseDirectory, xmlFile))
                {
                    s.IncludeXmlComments(xmlFilePath, true);
                }
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
