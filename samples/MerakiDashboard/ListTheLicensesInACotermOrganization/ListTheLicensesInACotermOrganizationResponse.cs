using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheLicensesInACotermOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheLicensesInACotermOrganizationResponse
    {
        public string Key { get; set; }
        public string OrganizationId { get; set; }
        public int Duration { get; set; }
        public string Mode { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime ClaimedAt { get; set; }
        public bool Invalidated { get; set; }
        public DateTime InvalidatedAt { get; set; }
        public bool Expired { get; set; }
        public List<Editions> Editions { get; set; }
        public List<Counts4> Counts { get; set; }
    }

    public class Counts4
    {
        public string Model { get; set; }
        public int Count { get; set; }
    }

    public class Editions
    {
        public string Edition { get; set; }
        public string ProductType { get; set; }
    }
}