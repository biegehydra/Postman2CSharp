using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<RebootADeviceResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class RebootADeviceResponse
    {
        public bool Success { get; set; }
    }
}