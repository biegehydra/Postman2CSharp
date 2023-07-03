using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<TifyThatCredentialHandoffToCameraHasCompletedRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class TifyThatCredentialHandoffToCameraHasCompletedRequest
    {
        public string Serial { get; set; }
        public string WirelessCredentialsSent { get; set; }
    }
}