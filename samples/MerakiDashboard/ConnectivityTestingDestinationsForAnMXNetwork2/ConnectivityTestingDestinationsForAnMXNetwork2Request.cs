using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ConnectivityTestingDestinationsForAnMXNetwork2Request>(myJsonResponse);
namespace MerakiDashboard
{
    public class ConnectivityTestingDestinationsForAnMXNetwork2Request
    {
        public List<Destinations2> Destinations { get; set; }
    }

    public class Destinations2
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string Default { get; set; }
    }
}