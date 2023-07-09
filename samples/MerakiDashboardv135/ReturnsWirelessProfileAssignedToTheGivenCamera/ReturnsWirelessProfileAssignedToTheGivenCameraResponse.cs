using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsWirelessProfileAssignedToTheGivenCameraResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnsWirelessProfileAssignedToTheGivenCameraResponse
    {
        public Ids Ids { get; set; }
    }

    public class Ids
    {
        public string Primary { get; set; }
        public string Secondary { get; set; }
        public string Backup { get; set; }
    }
}