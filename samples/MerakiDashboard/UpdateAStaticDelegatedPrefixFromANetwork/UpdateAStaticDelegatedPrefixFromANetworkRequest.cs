using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateAStaticDelegatedPrefixFromANetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateAStaticDelegatedPrefixFromANetworkRequest
    {
        public string Prefix { get; set; }
        public Origin Origin { get; set; }
        public string Description { get; set; }
    }
}