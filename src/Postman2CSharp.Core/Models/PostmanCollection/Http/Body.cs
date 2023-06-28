using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http
{
    public class Body
    {
        [JsonRequired] public required string Mode { get; set; }
        public string? Raw { get; set; }
        public GraphQl? Graphql { get; set; }
        public File? File { get; set; }
        public BodyOptions? Options { get; set; }
        public List<Formdata>? Formdata { get; set; }
        public List<Parameter>? Urlencoded { get; set; }
    }
    public class File
    {
        public string? Src { get; set; }
    }

    public class GraphQl
    {
        [JsonRequired]
        public required string Query { get; set; }
        [JsonRequired]
        public required string Variables { get; set; }
    }
}