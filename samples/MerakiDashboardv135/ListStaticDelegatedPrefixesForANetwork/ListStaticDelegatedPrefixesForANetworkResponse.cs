using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListStaticDelegatedPrefixesForANetworkResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListStaticDelegatedPrefixesForANetworkResponse
    {
        public string StaticDelegatedPrefixId { get; set; }
        public string Prefix { get; set; }
        public Origin2 Origin { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class Origin2
    {
        public string Type { get; set; }
        public List<string> Interfaces { get; set; }
    }
}