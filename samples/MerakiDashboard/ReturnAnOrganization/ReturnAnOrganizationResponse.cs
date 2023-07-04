using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnAnOrganizationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnAnOrganizationResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Wan2 Api { get; set; }
        public Uplinks Licensing { get; set; }
        public Cloud Cloud { get; set; }
        public Management Management { get; set; }
    }
}