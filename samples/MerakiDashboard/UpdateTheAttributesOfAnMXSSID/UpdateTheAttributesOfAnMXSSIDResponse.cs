using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheAttributesOfAnMXSSIDResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheAttributesOfAnMXSSIDResponse
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int DefaultVlanId { get; set; }
        public string AuthMode { get; set; }
        public List<RadiusServers> RadiusServers { get; set; }
        public string EncryptionMode { get; set; }
        public string WpaEncryptionMode { get; set; }
        public bool Visible { get; set; }
    }
}