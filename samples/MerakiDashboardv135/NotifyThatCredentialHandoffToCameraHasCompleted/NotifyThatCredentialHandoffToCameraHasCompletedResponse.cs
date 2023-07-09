using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<NotifyThatCredentialHandoffToCameraHasCompletedResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class NotifyThatCredentialHandoffToCameraHasCompletedResponse
    {
        public bool Success { get; set; }
    }
}