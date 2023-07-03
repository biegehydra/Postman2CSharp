using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateAHubBGPConfigurationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateAHubBGPConfigurationResponse
    {
        public bool Enabled { get; set; }
        public int AsNumber { get; set; }
        public int IbgpHoldTimer { get; set; }
        public List<Neighbors> Neighbors { get; set; }
    }
}