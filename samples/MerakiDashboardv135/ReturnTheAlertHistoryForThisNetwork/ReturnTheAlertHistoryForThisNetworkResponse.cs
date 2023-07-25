using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnTheAlertHistoryForThisNetworkResponse>>(myJsonResponse);
    public class ReturnTheAlertHistoryForThisNetworkResponse
    {
        public DateTime OccurredAt { get; set; }
        public string AlertTypeId { get; set; }
        public string AlertType { get; set; }
        public AggregatedConnectivityInfoForAGivenAPOnThisNetworkResponse Device { get; set; }
        public Destinations2 Destinations { get; set; }
    }

    public class Destinations2
    {
        public Email Email { get; set; }
        public Email Push { get; set; }
        public Email Sms { get; set; }
        public Email Webhook { get; set; }
    }

    public class Email
    {
        public DateTime SentAt { get; set; }
    }
}