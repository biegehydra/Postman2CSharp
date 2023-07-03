using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturntheDHCPsubnetinformationforanapplianceResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturntheDHCPsubnetinformationforanapplianceResponse
    {
        public string Subnet { get; set; }
        public int VlanId { get; set; }
        public int UsedCount { get; set; }
        public int FreeCount { get; set; }
    }
}