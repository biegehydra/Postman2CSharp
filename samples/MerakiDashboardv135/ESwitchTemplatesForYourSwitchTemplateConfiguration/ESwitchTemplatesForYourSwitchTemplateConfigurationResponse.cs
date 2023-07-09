using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ESwitchTemplatesForYourSwitchTemplateConfigurationResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ESwitchTemplatesForYourSwitchTemplateConfigurationResponse
    {
        public string SwitchProfileId { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
    }
}