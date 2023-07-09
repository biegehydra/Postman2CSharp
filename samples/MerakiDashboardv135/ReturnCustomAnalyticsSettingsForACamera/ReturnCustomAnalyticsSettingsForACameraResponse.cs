using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnCustomAnalyticsSettingsForACameraResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnCustomAnalyticsSettingsForACameraResponse
    {
        public bool Enabled { get; set; }
        public string ArtifactId { get; set; }
        public List<Parameters> Parameters { get; set; }
    }

    public class Parameters
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }
}