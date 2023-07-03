using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<EdConnectivityInfoForThisNetworkGroupedByNodeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class EdConnectivityInfoForThisNetworkGroupedByNodeResponse
    {
        public string Serial { get; set; }
        public ConnectionStats ConnectionStats { get; set; }
    }
}