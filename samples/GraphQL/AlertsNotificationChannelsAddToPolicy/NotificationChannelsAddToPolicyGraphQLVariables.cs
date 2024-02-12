using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewRelicAlerts
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<NotificationChannelsAddToPolicyGraphQLVariables>(myJsonResponse);
    public class NotificationChannelsAddToPolicyGraphQLVariables
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("notificationChannelIds")]
        public List<int> NotificationChannelIds { get; set; }

        [JsonPropertyName("policyId")]
        public int PolicyId { get; set; }
    }
}