using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AListOfAlertTypesToBeUsedWithManagingWebhookAlertsParameters>(myJsonResponse);
    public class AListOfAlertTypesToBeUsedWithManagingWebhookAlertsParameters : IQueryParameters
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