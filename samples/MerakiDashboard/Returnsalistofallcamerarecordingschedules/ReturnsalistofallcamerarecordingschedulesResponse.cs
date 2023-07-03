using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnsalistofallcamerarecordingschedulesResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnsalistofallcamerarecordingschedulesResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}