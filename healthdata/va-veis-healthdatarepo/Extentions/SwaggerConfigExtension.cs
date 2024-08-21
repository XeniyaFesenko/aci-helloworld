using Microsoft.OpenApi.Models;
using System.Reflection;

namespace va_veis_healthdatarepo.Middleware
{
    public static class SwaggerConfigExtension
    {
        /// <summary>
        /// Configures Swagger API documentation and adds it to the specified service collection.
        /// </summary>
        /// <param name="services">The service collection to add Swagger documentation to.</param>
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "va-veis-healthdatarepo-dev",
                    Version = "v1",
                    Description = "A Template API based upon the\r\nFMC Mulesoft App Service.",                   
                    Contact = new OpenApiContact
                    {
                        Name = "VEIS Team",
                        Email = "vaoitdsovrmveisteam@va.gov"
                    }
                });

                // Token configuration

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                //services.AddControllers();
            });

        }

        /// <summary>
        /// Configures the Swagger middleware to generate and serve Swagger documentation for the API.
        /// </summary>
        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Healthdatarepo API");
            });
        }
    }
}