using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ConnectivityTestingDestinationsForAnMGNetwork2Request>(myJsonResponse);
namespace MerakiDashboard
{
    public class ConnectivityTestingDestinationsForAnMGNetwork2Request
    {
        public List<Destinations5> Destinations { get; set; }
    }

    public class Destinations5
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public bool Default { get; set; }
    }
}