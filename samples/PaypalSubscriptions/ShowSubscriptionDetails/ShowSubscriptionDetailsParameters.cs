using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PaypalSubscription
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ShowSubscriptionDetailsParameters>(myJsonResponse);
    public class ShowSubscriptionDetailsParameters : IQueryParameters
    {
        /// <summary>
        /// List of fields that are to be returned in the response. Possible value for fields are last_failed_payment 
        /// and plan.
        /// </summary>
        public string Fields { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "fields",
                    Fields
                }
            };
        }
    }
}