using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ConnectivityTestingDestinationsForAnMGNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ConnectivityTestingDestinationsForAnMGNetworkResponse
    {
        public List<Destinations> Destinations { get; set; }
    }
}