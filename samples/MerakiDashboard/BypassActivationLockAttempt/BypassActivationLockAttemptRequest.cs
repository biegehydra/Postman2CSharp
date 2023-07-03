using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<BypassActivationLockAttemptRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class BypassActivationLockAttemptRequest
    {
        public List<string> Ids { get; set; }
    }
}