using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<BypassActivationLockAttemptResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class BypassActivationLockAttemptResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public _9 Data { get; set; }
    }
}