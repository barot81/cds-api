using Exxat.SyncJobs.RoleAccessJob.ServiceCollectionExntesion;
using MediatR;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Zhealthcare.Service.Domain.Entities;
using Zhealthcare.Service.Domain.Entities.Drg;
using Zhealthcare.Service.Domain.Entities.Lookup;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Configurations
{
    public static class PatientModuleServiceCollection
    {
        public static IServiceCollection AddPatientModule(
            this IServiceCollection services,
            IConfiguration config)
        => services
        .InitializePatientModuleMediator()
        .InitializeCosmosRepository(config)
        .AddUserContext();

        private static IServiceCollection AddUserContext(this IServiceCollection services)
        => services.AddScoped(provider =>
            {
                var contextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
                var userName = contextAccessor?.HttpContext?.User?.Claims?
                    .FirstOrDefault(x => x.Type == "preferred_username")?.Value;
                if (string.IsNullOrEmpty(userName))
                {
                    var token = contextAccessor?.HttpContext?.Request?.Headers?["Authorization"].ToString()?.Replace("Bearer ", "");
                    var handler = new JwtSecurityTokenHandler();
                    if (handler.ReadToken(token) is JwtSecurityToken jsonToken)
                    {
                        var preferredUsername = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "preferred_username")?.Value;
                        return new UserContext(string.IsNullOrEmpty(preferredUsername) ? string.Empty : preferredUsername);
                    }
                }
                return new UserContext(userName ?? string.Empty);
            });

        private static IServiceCollection InitializePatientModuleMediator(
            this IServiceCollection services)
            => services.AddMediatR(typeof(PatientModuleServiceCollection));

        private static IServiceCollection InitializeCosmosRepository(
            this IServiceCollection services,
            IConfiguration config)
        {
            var cosmosConfig = config.GetSection(Constants.Configuration.CosmosKey).Get<ConnectionConfig>();
            services.AddCosmosRepository(
                options =>
                {
                    options.CosmosConnectionString = $"AccountEndpoint={cosmosConfig!.EndpointUrl};AccountKey={cosmosConfig!.AuthorizationKey};";
                    options.DatabaseId = cosmosConfig?.Connections?["Patients"].DatabaseName ?? string.Empty;
                    options.ContainerPerItemType = true;
                    string lookupContainer = cosmosConfig?.Connections?["Lookups"].CollectionName ?? string.Empty;
                    string patientContainer = cosmosConfig?.Connections?["Patients"].CollectionName ?? string.Empty;
                    options.ContainerBuilder
                    .Configure<Patient>(builder =>
                    {
                        builder.WithoutStrictTypeChecking().WithContainer(patientContainer);
                    })
                    .Configure<PatientFinding>(builder =>
                    {
                        builder.WithoutStrictTypeChecking().WithContainer(patientContainer);
                    })
                    .Configure<Lookup<MsDrgLookupItem>>(builder =>
                    {
                        builder.WithContainer(lookupContainer).WithoutStrictTypeChecking();
                    }).Configure<Lookup<AprDrgLookupItem>>(builder =>
                    {
                        builder.WithContainer(lookupContainer).WithoutStrictTypeChecking();
                    }).Configure<Lookup<ReimbursementTypeLookupItem>>(builder =>
                    {
                        builder.WithContainer(lookupContainer).WithoutStrictTypeChecking();
                    }).Configure<Lookup<DiagnosisLookupItem>>(builder =>
                    {
                        builder.WithContainer(lookupContainer).WithoutStrictTypeChecking();
                    });

                });
            return services;
        }
    }
}
