using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ESwitchTemplatesForYourSwitchTemplateConfigurationResponse>>(myJsonResponse);
    public class ESwitchTemplatesForYourSwitchTemplateConfigurationResponse
    {
        public string SwitchProfileId { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
    }
}