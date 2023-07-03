using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<TifyThatCredentialHandoffToCameraHasCompletedResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class TifyThatCredentialHandoffToCameraHasCompletedResponse
    {
        public bool Success { get; set; }
    }
}