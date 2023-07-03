using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnthealerthistoryforthisnetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnthealerthistoryforthisnetworkResponse
    {
        public DateTime OccurredAt { get; set; }
        public string AlertTypeId { get; set; }
        public string AlertType { get; set; }
        public Device3 Device { get; set; }
        public Destinations7 Destinations { get; set; }
    }

    public class Destinations7
    {
        public Email4 Email { get; set; }
        public Push Push { get; set; }
        public Sms Sms { get; set; }
        public Webhook Webhook { get; set; }
    }

    public class Device3
    {
        public string Serial { get; set; }
    }

    public class Email4
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