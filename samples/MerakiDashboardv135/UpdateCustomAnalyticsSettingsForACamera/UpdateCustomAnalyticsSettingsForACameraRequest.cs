using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateCustomAnalyticsSettingsForACameraRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class UpdateCustomAnalyticsSettingsForACameraRequest
    {
        public string Enabled { get; set; }
        public string ArtifactId { get; set; }
        public List<Parameters2> Parameters { get; set; }
    }

    public class Parameters2
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}