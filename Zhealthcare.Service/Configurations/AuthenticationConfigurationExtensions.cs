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
                        options.Events = new JwtBearerEvents();

                        /// <summary>
                        /// Below you can do extended token validation and check for additional claims, such as:
                        ///
                        /// - check if the caller's tenant is in the allowed tenants list via the 'tid' claim (for multi-tenant applications)
                        /// - check if the caller's account is homed or guest via the 'acct' optional claim
                        /// - check if the caller belongs to right roles or groups via the 'roles' or 'groups' claim, respectively
                        ///
                        /// Bear in mind that you can do any of the above checks within the individual routes and/or controllers as well.
                        /// For more information, visit: https://docs.microsoft.com/azure/active-directory/develop/access-tokens#validate-the-user-has-permission-to-access-this-data
                        /// </summary>

                        options.Events.OnTokenValidated = async context =>
                        {
                            string[] allowedClientApps = { "ccdf8d9f-bd27-49d3-8239-8f37dcaeae99" };

                            string clientappId = context?.Principal?.Claims
                                .FirstOrDefault(x => x.Type == "azp" || x.Type == "appid")?.Value;

                            if (!allowedClientApps.Contains(clientappId))
                            {
                                throw new System.Exception("This client is not authorized");
                            }
                        };
                        options.Events.OnMessageReceived = async context =>
                        {
                            //throw new System.Exception("This client is not authorized");
                        };
                        options.Events.OnMessageReceived = async context =>
                        {
                            //throw new System.Exception("This client is not authorized");
                        };
                        options.Events.OnChallenge = async context =>
                        {
                        };
                        options.Events.OnAuthenticationFailed = async context =>
                        {
                            string[] allowedClientApps = { /* list of client ids to allow */ };

                            string clientappId = context?.Principal?.Claims
                                .FirstOrDefault(x => x.Type == "azp" || x.Type == "appid")?.Value;

                            if (!allowedClientApps.Contains(clientappId))
                            {
                                throw new System.Exception("This client is not authorized");
                            }
                        };
                    }, options => { Configuration.Bind("AzureAd", options); });
            services.AddAuthorization();
            // The following flag can be used to get more descriptive errors in development environments
            IdentityModelEventSource.ShowPII = false;
            return services;

        }

    }
}
