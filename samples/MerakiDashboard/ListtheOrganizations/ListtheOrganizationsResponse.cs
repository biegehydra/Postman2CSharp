using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListtheOrganizationsResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListtheOrganizationsResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}