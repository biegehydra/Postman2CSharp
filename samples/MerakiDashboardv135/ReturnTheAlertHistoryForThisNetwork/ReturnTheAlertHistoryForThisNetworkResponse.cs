using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnTheAlertHistoryForThisNetworkResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnTheAlertHistoryForThisNetworkResponse
    {
        public DateTime OccurredAt { get; set; }
        public string AlertTypeId { get; set; }
        public string AlertType { get; set; }
        public Device Device { get; set; }
        public Destinations2 Destinations { get; set; }
    }

    public class Destinations2
    {
        public Email2 Email { get; set; }
        public Push Push { get; set; }
        public Sms Sms { get; set; }
        public Webhook Webhook { get; set; }
    }

    public class Email2
    {
        public DateTime SentAt { get; set; }
    }

    public class Push
    {
        public DateTime SentAt { get; set; }
    }

    public class Sms
    {
        public DateTime SentAt { get; set; }
    }

    public class Webhook
    {
        public DateTime SentAt { get; set; }
    }
}