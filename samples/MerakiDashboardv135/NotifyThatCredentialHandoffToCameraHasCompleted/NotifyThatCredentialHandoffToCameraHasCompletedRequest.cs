using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<NotifyThatCredentialHandoffToCameraHasCompletedRequest>(myJsonResponse);
    public class NotifyThatCredentialHandoffToCameraHasCompletedRequest
    {
        public string Serial { get; set; }
        public string WirelessCredentialsSent { get; set; }
    }
}