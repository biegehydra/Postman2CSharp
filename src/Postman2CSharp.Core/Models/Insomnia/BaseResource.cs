using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.Insomnia
{
    public abstract class BaseResource
    {
        [JsonRequired]
        public required string ParentId { get; set; }

        [JsonRequired]
        public required string Name { get; set; }

        [JsonRequired]
        public required long Modified { get; set; }

        [JsonRequired]
        public required long Created { get; set; }

        [JsonRequired]
        public long MetaSortKey { get; set; }
    }
}
