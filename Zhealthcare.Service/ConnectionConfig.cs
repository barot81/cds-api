namespace Exxat.SyncJobs.RoleAccessJob.ServiceCollectionExntesion
{
    public class ConnectionConfig
    {
        public string? EndpointUrl { get; init; }
        public string? AuthorizationKey { get; init; }

        public ContainerInfo? Connections { get; set; }
    }

    public record ContainerInfo
    {
        public string? DatabaseName { get; init; }

        public string? CollectionName { get; init; }
    }
}
