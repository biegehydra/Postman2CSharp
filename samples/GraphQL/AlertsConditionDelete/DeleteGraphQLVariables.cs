using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewRelicAlerts
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<DeleteGraphQLVariables>(myJsonResponse);
    public class DeleteGraphQLVariables
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}