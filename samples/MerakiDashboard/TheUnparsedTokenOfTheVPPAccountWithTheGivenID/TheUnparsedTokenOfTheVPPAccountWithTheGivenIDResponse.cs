using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<TheUnparsedTokenOfTheVPPAccountWithTheGivenIDResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class TheUnparsedTokenOfTheVPPAccountWithTheGivenIDResponse
    {
        public string Id { get; set; }
        public string VppServiceToken { get; set; }
    }
}