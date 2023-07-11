using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.Insomnia
{
    public class Parameter
    {
        [JsonRequired]
        public required string Name { get; set; }
        [JsonRequired]
        public required string Value { get; set; }
        [JsonRequired]
        public required bool Disabled { get; set; }
    }
}
