using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EturnsWirelessProfileAssignedToTheGivenCameraResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class EturnsWirelessProfileAssignedToTheGivenCameraResponse
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