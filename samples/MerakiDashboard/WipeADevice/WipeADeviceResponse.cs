using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<WipeADeviceResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class WipeADeviceResponse
    {
        public string Id { get; set; }
    }
}