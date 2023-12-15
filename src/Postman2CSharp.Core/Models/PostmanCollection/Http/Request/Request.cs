using System.Collections.Generic;
using System.Text.Json.Serialization;
using Postman2CSharp.Core.Models.PostmanCollection.Authorization;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http.Request
{
    public class Request
    {
        [JsonRequired] public required string Method { get; set; }
        [JsonRequired] public required List<Header> Header { get; set; }
        public Url Url { get; set; } = null!;
        public AuthSettings? Auth { get; set; }
        public Body? Body { get; set; }
        public string? Description { get; set; }
    }
}