using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ShowsubscriptiondetailsResponse>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class Links
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
    }

    public class ShowsubscriptiondetailsResponse
    {
        public string Status { get; set; }
        public string Id { get; set; }
        public string PlanId { get; set; }
        public DateTime StartTime { get; set; }
        public string Quantity { get; set; }
        public DateTime CreateTime { get; set; }
        public bool PlanOverridden { get; set; }
        public List<Links> Links { get; set; }
    }
}