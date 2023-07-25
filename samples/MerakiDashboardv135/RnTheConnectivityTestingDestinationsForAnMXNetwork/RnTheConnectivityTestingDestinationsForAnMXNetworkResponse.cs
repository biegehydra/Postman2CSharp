using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<RnTheConnectivityTestingDestinationsForAnMXNetworkResponse>(myJsonResponse);
    public class RnTheConnectivityTestingDestinationsForAnMXNetworkResponse
    {
        public List<Destinations> Destinations { get; set; }
    }

    public class Destinations
    {
        public string Ip { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }
    }
}