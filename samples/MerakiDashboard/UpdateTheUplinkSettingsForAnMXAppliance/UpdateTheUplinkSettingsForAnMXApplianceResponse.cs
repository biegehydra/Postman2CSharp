using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheUplinkSettingsForAnMXApplianceResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheUplinkSettingsForAnMXApplianceResponse
    {
        public Interfaces Interfaces { get; set; }
    }
}