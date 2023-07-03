using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ConnectivityTestingDestinationsForAnMXNetwork2Response>(myJsonResponse);
namespace MerakiDashboard
{
    public class ConnectivityTestingDestinationsForAnMXNetwork2Response
    {
        public List<Destinations> Destinations { get; set; }
    }
}