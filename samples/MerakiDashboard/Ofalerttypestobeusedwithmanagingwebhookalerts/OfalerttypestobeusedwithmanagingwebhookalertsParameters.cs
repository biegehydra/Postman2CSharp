using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<OfAlertTypesToBeUsedWithManagingWebhookAlertsParameters>(myJsonResponse);
namespace MerakiDashboard
{
    public class OfAlertTypesToBeUsedWithManagingWebhookAlertsParameters : IQueryParameters
    {
        /// <summary>
        /// Filter sample alerts to a specific product type
        /// </summary>
        public string ProductType { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "productType",
                    ProductType
                }
            };
        }
    }
}