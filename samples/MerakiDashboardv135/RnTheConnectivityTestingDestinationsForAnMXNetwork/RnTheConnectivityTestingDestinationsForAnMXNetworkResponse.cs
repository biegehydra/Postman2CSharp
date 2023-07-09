using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<RnTheConnectivityTestingDestinationsForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
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