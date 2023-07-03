using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnAHubBGPConfigurationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnAHubBGPConfigurationResponse
    {
        public bool Enabled { get; set; }
        public int AsNumber { get; set; }
        public int IbgpHoldTimer { get; set; }
        public List<Neighbors> Neighbors { get; set; }
    }

    public class Neighbors
    {
        public string Ip { get; set; }
        public int RemoteAsNumber { get; set; }
        public int ReceiveLimit { get; set; }
        public bool AllowTransit { get; set; }
        public int EbgpHoldTimer { get; set; }
        public int EbgpMultihop { get; set; }
    }
}