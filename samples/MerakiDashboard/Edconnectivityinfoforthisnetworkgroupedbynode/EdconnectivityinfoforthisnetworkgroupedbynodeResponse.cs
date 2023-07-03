using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<EdconnectivityinfoforthisnetworkgroupedbynodeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class EdconnectivityinfoforthisnetworkgroupedbynodeResponse
    {
        public string Serial { get; set; }
        public ConnectionStats ConnectionStats { get; set; }
    }
}