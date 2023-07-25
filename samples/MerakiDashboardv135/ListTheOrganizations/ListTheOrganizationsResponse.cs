using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheOrganizationsResponse>>(myJsonResponse);
    public class ListTheOrganizationsResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}