using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AssignWirelessProfilesToTheGivenCameraRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class AssignWirelessProfilesToTheGivenCameraRequest
    {
        public Ids Ids { get; set; }
    }
}