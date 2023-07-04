using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<GenerateANewVMXAuthenticationTokenResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class GenerateANewVMXAuthenticationTokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}