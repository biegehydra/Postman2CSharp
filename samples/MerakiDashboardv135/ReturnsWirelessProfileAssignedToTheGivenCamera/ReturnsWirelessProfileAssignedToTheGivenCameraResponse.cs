using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsWirelessProfileAssignedToTheGivenCameraResponse>(myJsonResponse);
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