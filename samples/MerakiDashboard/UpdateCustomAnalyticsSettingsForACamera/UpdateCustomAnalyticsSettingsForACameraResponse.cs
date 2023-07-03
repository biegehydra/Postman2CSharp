using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateCustomAnalyticsSettingsForACameraResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateCustomAnalyticsSettingsForACameraResponse
    {
        public bool Enabled { get; set; }
        public string ArtifactId { get; set; }
        public List<Parameters> Parameters { get; set; }
    }
}