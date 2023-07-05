using System.Collections.Generic;
using System.Text.Json.Serialization;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Request;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http.Response
{
    public class Response
    {
        [JsonRequired] public required string Name { get; set; }
        [JsonRequired] public required OriginalRequest OriginalRequest { get; set; }
        public List<Header>? Header { get; set; }
        public List<object>? Cookie { get; set; }
        public string? PostmanPreviewlanguage { get; set; }
        public string? Status { get; set; }
        public string? Body { get; set; }
        public int? Code { get; set; }
        public bool IsSuccessCode => Code is >= 200 and < 300; 
    }
}