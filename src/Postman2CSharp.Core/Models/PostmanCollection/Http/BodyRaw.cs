using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http
{
    public class BodyRaw
    {
        [JsonRequired] public required string Language { get; set; }
    }
}