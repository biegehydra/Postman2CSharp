using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListTransactionsForSubscriptionParameters>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class ListTransactionsForSubscriptionParameters : IQueryParameters
    {
        /// <summary>
        /// (Required) The start time of the range of transactions to list.
        /// </summary>
        public string StartTime { get; set; }
        
        /// <summary>
        /// (Required) The end time of the range of transactions to list.
        /// </summary>
        public string EndTime { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "start_time",
                    StartTime
                },
                {
                    "end_time",
                    EndTime
                }
            };
        }
    }
}