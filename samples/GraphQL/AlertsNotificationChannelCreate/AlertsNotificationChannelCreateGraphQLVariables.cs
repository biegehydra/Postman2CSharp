using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewRelicAlerts
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AlertsNotificationChannelCreateGraphQLVariables>(myJsonResponse);
    public class AlertsNotificationChannelCreateGraphQLVariables
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("notificationChannel")]
        public NotificationChannel NotificationChannel { get; set; }
    }

    public class NotificationChannel
    {
        [JsonPropertyName("email")]
        public Email Email { get; set; }

        [JsonPropertyName("opsGenie")]
        public OpsGenie OpsGenie { get; set; }

        [JsonPropertyName("pagerDuty")]
        public PagerDuty PagerDuty { get; set; }

        [JsonPropertyName("slack")]
        public Slack Slack { get; set; }

        [JsonPropertyName("victorOps")]
        public VictorOps VictorOps { get; set; }

        [JsonPropertyName("webhook")]
        public Webhook Webhook { get; set; }

        [JsonPropertyName("xMatters")]
        public XMatters XMatters { get; set; }
    }

    public class OpsGenie
    {
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; }

        [JsonPropertyName("dataCenterRegion")]
        public string DataCenterRegion { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("recipients")]
        public List<string> Recipients { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [JsonPropertyName("teams")]
        public List<string> Teams { get; set; }
    }

    public class Webhook
    {
        [JsonPropertyName("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonPropertyName("basicAuth")]
        public BasicAuth BasicAuth { get; set; }

        [JsonPropertyName("customHttpHeaders")]
        public CustomHttpHeaders CustomHttpHeaders { get; set; }

        [JsonPropertyName("customPayloadBody")]
        public string CustomPayloadBody { get; set; }

        [JsonPropertyName("customPayloadType")]
        public string CustomPayloadType { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Email
    {
        [JsonPropertyName("emails")]
        public List<string> Emails { get; set; }

        [JsonPropertyName("includeJson")]
        public bool IncludeJson { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Slack
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("teamChannel")]
        public string TeamChannel { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class VictorOps
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("routeKey")]
        public string RouteKey { get; set; }
    }

    public class BasicAuth
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }

    public class CustomHttpHeaders
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class PagerDuty
    {
        [JsonPropertyName("apiKey")]
        public string ApiKey { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class XMatters
    {
        [JsonPropertyName("integrationUrl")]
        public string IntegrationUrl { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}