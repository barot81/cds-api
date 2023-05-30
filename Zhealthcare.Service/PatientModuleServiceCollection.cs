using Exxat.SyncJobs.RoleAccessJob.ServiceCollectionExntesion;
using Mapster;
using MediatR;
using Zhealthcare.Service.Domain.Entities;
using Zhealthcare.Service.Domain.Entities.Drg;
using Zhealthcare.Service.Domain.Entities.Lookup;

namespace Zhealthcare.Service
{
    public static class PatientModuleServiceCollection
    {
        public static IServiceCollection AddPatientModule(
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
                    options.DatabaseId = cosmosConfig?.Connections?["Patients"].DatabaseName ?? string.Empty;
                    options.ContainerId = cosmosConfig?.Connections?["Patients"].CollectionName ?? string.Empty;
                    options.ContainerBuilder
                    .Configure<Patient>(builder => 
                    {
                        builder.WithoutStrictTypeChecking();
                    })
                    .Configure<PatientFinding>(builder =>
                    {
                        builder.WithoutStrictTypeChecking();
                    })
                    .Configure<Lookup<MsDrgLookupItem>>(builder =>
                    {
                        builder.WithContainer(cosmosConfig?.Connections?["Lookups"].CollectionName ?? string.Empty);
                        builder.WithoutStrictTypeChecking();
                    }).Configure<Lookup<AprDrgLookupItem>>(builder =>
                    {
                        builder.WithContainer(cosmosConfig?.Connections?["Lookups"].CollectionName ?? string.Empty);
                        builder.WithoutStrictTypeChecking();
                    }).Configure<Lookup<ReimbursementTypeLookupItem>>(builder =>
                    {
                        builder.WithContainer(cosmosConfig?.Connections?["Lookups"].CollectionName ?? string.Empty);
                        builder.WithoutStrictTypeChecking();
                    }).Configure<Lookup<DiagnosisLookupItem>>(builder =>
                    {
                        builder.WithContainer(cosmosConfig?.Connections?["Lookups"].CollectionName ?? string.Empty);
                        builder.WithoutStrictTypeChecking();
                    });
                
                });
            return services;
        }
    }
}
