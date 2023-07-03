using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateMulticastSettingsForANetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateMulticastSettingsForANetworkResponse
    {
        public DefaultSettings DefaultSettings { get; set; }
        public List<Overrides> Overrides { get; set; }
    }
}