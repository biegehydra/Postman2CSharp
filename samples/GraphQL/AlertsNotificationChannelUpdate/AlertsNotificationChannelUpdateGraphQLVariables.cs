using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewRelicAlerts
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AlertsNotificationChannelUpdateGraphQLVariables>(myJsonResponse);
    public class AlertsNotificationChannelUpdateGraphQLVariables
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("notificationChannel")]
        public NotificationChannel NotificationChannel { get; set; }
    }
}