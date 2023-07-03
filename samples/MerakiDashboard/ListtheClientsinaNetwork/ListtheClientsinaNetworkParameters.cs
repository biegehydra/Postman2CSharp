using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListtheClientsinaNetworkParameters>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListtheClientsinaNetworkParameters : IQueryParameters
    {
        /// <summary>
        /// The beginning of the timespan for the data. The maximum lookback period is 31 days from today.
        /// </summary>
        public string T0 { get; set; }
        
        /// <summary>
        /// The timespan for which the information will be fetched. If specifying timespan, do not specify parameter t0. The value must be in seconds and be less than or equal to 31 days. The default is 1 day.
        /// </summary>
        public string Timespan { get; set; }
        
        /// <summary>
        /// The number of entries per page returned. Acceptable range is 3 - 1000. Default is 10.
        /// </summary>
        public string PerPage { get; set; }
        
        /// <summary>
        /// A token used by the server to indicate the start of the page. Often this is a timestamp or an ID but it is not limited to those. This parameter should not be defined by client applications. The link for the first, last, prev, or next page in the HTTP Link header should define it.
        /// </summary>
        public string StartingAfter { get; set; }
        
        /// <summary>
        /// A token used by the server to indicate the end of the page. Often this is a timestamp or an ID but it is not limited to those. This parameter should not be defined by client applications. The link for the first, last, prev, or next page in the HTTP Link header should define it.
        /// </summary>
        public string EndingBefore { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "t0",
                    T0
                },
                {
                    "timespan",
                    Timespan
                },
                {
                    "perPage",
                    PerPage
                },
                {
                    "startingAfter",
                    StartingAfter
                },
                {
                    "endingBefore",
                    EndingBefore
                }
            };
        }
    }
}