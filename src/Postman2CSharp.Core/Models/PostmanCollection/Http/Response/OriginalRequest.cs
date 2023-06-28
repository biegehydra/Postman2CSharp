using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http.Response
{
    public class OriginalRequest
    {
        public Url? Url { get; set; }
        [JsonRequired] public required string Method { get; set; }
        [JsonRequired] public required List<Header> Header { get; set; }
        private HttpMethod HttpMethod => new HttpMethod(Method);
        public Body? Body { get; set; }
    }
}