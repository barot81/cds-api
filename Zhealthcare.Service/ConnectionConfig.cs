namespace Exxat.SyncJobs.RoleAccessJob.ServiceCollectionExntesion
{
    public class ConnectionConfig
    {
        public ConnectionConfig()
        {
            this.Connections = new();
        }
        public string? EndpointUrl { get; init; }
        public string? AuthorizationKey { get; init; }

        public Dictionary<string, ContainerInfo> Connections { get; set; }
    }

    public record ContainerInfo
    {
        public string? DatabaseName { get; init; }

        public string? CollectionName { get; init; }
    }
}
