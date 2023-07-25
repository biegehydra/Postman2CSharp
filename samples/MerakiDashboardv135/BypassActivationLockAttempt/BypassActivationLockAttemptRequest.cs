using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<BypassActivationLockAttemptRequest>(myJsonResponse);
    public class BypassActivationLockAttemptRequest
    {
        public List<string> Ids { get; set; }
    }
}