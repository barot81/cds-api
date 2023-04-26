using Exxat.SyncJobs.RoleAccessJob.ServiceCollectionExntesion;
using MediatR;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service
{
    public static class PatientModuleServiceCollection
    {
        internal static IServiceCollection AddPatientModule(
            this IServiceCollection services,
            IConfiguration config)
        => services
        .InitializePatientModuleMediator()
        .InitializeCosmosRepository(config);



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
                    options.ContainerId = cosmosConfig?.Connections?.CollectionName ?? string.Empty;
                    options.DatabaseId = cosmosConfig?.Connections?.DatabaseName ?? string.Empty;
                    options.ContainerBuilder.Configure<Patient>(builder =>
                    {
                        builder.WithoutStrictTypeChecking();
                    });
                });
            return services;
        }
    }
}
