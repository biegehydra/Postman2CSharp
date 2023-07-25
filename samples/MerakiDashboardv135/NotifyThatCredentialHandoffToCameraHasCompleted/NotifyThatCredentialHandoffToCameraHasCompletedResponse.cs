using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<NotifyThatCredentialHandoffToCameraHasCompletedResponse>(myJsonResponse);
    public class NotifyThatCredentialHandoffToCameraHasCompletedResponse
    {
        public bool Success { get; set; }
    }
}