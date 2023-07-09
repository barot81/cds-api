using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Zhealthcare.Service.Configurations
{
    public static class SwaggerConfigurationExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection Services)
        {
            Services.AddSwaggerGen(c =>
         {
             c.SwaggerDoc("v1", new OpenApiInfo { Title = "Exxat.Platform.EmailService", Version = "v1" });

             //Security scheme
             c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                 new OpenApiSecurityScheme
                 {
                     Description = "JWT Authorization header using the Bearer scheme.",
                     Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                     Scheme = JwtBearerDefaults.AuthenticationScheme //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                 });

             c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{
                            Id = JwtBearerDefaults.AuthenticationScheme, //The name of the previously defined security scheme.
                            Type = ReferenceType.SecurityScheme
                        }
                    },new List<string>()
                }
        });
         });
            return Services;
        }
    }
}
