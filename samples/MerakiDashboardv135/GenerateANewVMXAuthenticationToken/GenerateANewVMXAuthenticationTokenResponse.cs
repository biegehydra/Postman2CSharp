using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<GenerateANewVMXAuthenticationTokenResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class GenerateANewVMXAuthenticationTokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}