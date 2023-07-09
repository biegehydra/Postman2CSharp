using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<NotifyThatCredentialHandoffToCameraHasCompletedRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class NotifyThatCredentialHandoffToCameraHasCompletedRequest
    {
        public string Serial { get; set; }
        public string WirelessCredentialsSent { get; set; }
    }
}