using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<EdNetworkIntoIndividualNetworksForEachTypeOfDeviceResponse>(myJsonResponse);
    public class EdNetworkIntoIndividualNetworksForEachTypeOfDeviceResponse
    {
        public List<ReturnANetworkResponse> ResultingNetworks { get; set; }
    }
}