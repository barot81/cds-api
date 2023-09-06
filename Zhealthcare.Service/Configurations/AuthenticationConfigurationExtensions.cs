using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;

namespace Zhealthcare.Service.Configurations
{
    public static class AuthenticationConfigurationExtensions
    {
        public static IServiceCollection AddAzureAdAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(options =>
                    {
                        Configuration.Bind("AzureAd", options);
                        options.Events = new JwtBearerEvents
                        {
                            OnTokenValidated = async context =>
                            {
                                string[] allowedClientApps = { Configuration.GetValue<string>("AzureAd:AllowedClientId") ?? "" };

                                string? clientappId = context?.Principal?.Claims
                                    .FirstOrDefault(x => x.Type == "azp" || x.Type == "appid")?.Value;

                                if (!allowedClientApps.Contains(clientappId))
                                    throw new Exception("This client is not authorized");

                            }
                        };
                    }, options => { Configuration.Bind("AzureAd", options); });
            services.AddAuthorization();
            IdentityModelEventSource.ShowPII = false;
            return services;

        }

    }
}
