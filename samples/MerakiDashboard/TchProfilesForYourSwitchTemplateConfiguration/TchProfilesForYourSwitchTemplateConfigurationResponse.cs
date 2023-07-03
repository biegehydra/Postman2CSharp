using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<TchProfilesForYourSwitchTemplateConfigurationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class TchProfilesForYourSwitchTemplateConfigurationResponse
    {
        public string SwitchProfileId { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
    }
}