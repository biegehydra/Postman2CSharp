using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListTheTargetGroupsInThisNetworkParameters>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheTargetGroupsInThisNetworkParameters : IQueryParameters
    {
        /// <summary>
        /// Boolean indicating if the the ids of the devices or users scoped by the target group should be included in the response
        /// </summary>
        public string WithDetails { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "withDetails",
                    WithDetails
                }
            };
        }
    }
}