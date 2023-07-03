using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AssignWirelessProfilesToTheGivenCameraResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class AssignWirelessProfilesToTheGivenCameraResponse
    {
        public Ids Ids { get; set; }
    }
}