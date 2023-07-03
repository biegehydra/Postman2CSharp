using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UnenrollADeviceResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UnenrollADeviceResponse
    {
        public bool Success { get; set; }
    }
}