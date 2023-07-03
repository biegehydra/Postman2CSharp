using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnthelogofwebhookPOSTssentResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnthelogofwebhookPOSTssentResponse
    {
        public string AlertType { get; set; }
        public DateTime LoggedAt { get; set; }
        public string NetworkId { get; set; }
        public string OrganizationId { get; set; }
        public int ResponseCode { get; set; }
        public int ResponseDuration { get; set; }
        public DateTime SentAt { get; set; }
        public string Url { get; set; }
    }
}