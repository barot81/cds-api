using Microsoft.Azure.CosmosRepository;
using Newtonsoft.Json;

namespace Zhealthcare.Service.Domain.Entities
{
    public class BaseEntity: Item
    {
        /// <summary>
        /// Gets or sets the item's globally unique identifier.
        /// </summary>
        /// <remarks>
        /// Initialized by <see cref="Guid.NewGuid"/>.
        /// </remarks>
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the item's type name. This is used as a discriminator.
        /// </summary>
        [JsonProperty("entityName")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("partitionKey")]
        public virtual string PartitionKey { get; set; } = string.Empty;

    }
}
