using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<CreateANewOrganizationResponse>(myJsonResponse);
    public class CreateANewOrganizationResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Aggregation Api { get; set; }
        public ByModel Licensing { get; set; }
        public Cloud Cloud { get; set; }
        public Management Management { get; set; }
    }

    public class Cloud
    {
        public Parameters2 Region { get; set; }
    }
}