using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<AListOfAlertTypesToBeUsedWithManagingWebhookAlertsResponse>>(myJsonResponse);
    public class AListOfAlertTypesToBeUsedWithManagingWebhookAlertsResponse
    {
        public string AlertTypeId { get; set; }
        public string AlertType { get; set; }
        public string Version { get; set; }
        public string SharedSecret { get; set; }
        public DateTime SentAt { get; set; }
        public string AlertId { get; set; }
        public string AlertLevel { get; set; }
        public DateTime OccurredAt { get; set; }
        public Ac AlertData { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationUrl { get; set; }
        public string DeviceSerial { get; set; }
        public string DeviceMac { get; set; }
        public string DeviceName { get; set; }
        public string DeviceUrl { get; set; }
        public List<string> DeviceTags { get; set; }
        public string DeviceModel { get; set; }
        public string NetworkId { get; set; }
        public string NetworkName { get; set; }
        public string NetworkUrl { get; set; }
        public string EnrollmentString { get; set; }
        public string Notes { get; set; }
        public List<string> ProductTypes { get; set; }
    }
}