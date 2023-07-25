using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<BypassActivationLockAttemptResponse>(myJsonResponse);
    public class BypassActivationLockAttemptResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public Ac Data { get; set; }
    }
}