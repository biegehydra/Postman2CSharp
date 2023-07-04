using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AtionOfTheOrganizationsAPIDocumentationInJSONResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class AtionOfTheOrganizationsAPIDocumentationInJSONResponse
    {
        public string Openapi { get; set; }
        public Info Info { get; set; }
        public Paths Paths { get; set; }
    }

    public class Get
    {
        public string Description { get; set; }
        public string OperationId { get; set; }
        public Responses Responses { get; set; }
    }

    public class Info
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class _200
    {
        public string Description { get; set; }
        public Examples Examples { get; set; }
    }

    public class Examples
    {
        [JsonPropertyName("application/json")]
        public List<ResultingNetworks> ApplicationJson { get; set; }
    }

    public class Organizations
    {
        public Get Get { get; set; }
    }

    public class Paths
    {
        [JsonPropertyName("/organizations")]
        public Organizations Organizations { get; set; }
    }

    public class Responses
    {
        [JsonPropertyName("200")]
        public _200 _200 { get; set; }
    }
}