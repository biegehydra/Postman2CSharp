using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheOrganizationsResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheOrganizationsResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}