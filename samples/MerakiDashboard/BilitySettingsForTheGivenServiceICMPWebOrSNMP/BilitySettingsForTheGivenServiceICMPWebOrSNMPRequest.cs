using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<BilitySettingsForTheGivenServiceICMPWebOrSNMPRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class BilitySettingsForTheGivenServiceICMPWebOrSNMPRequest
    {
        public string Access { get; set; }
        public List<string> AllowedIps { get; set; }
    }
}