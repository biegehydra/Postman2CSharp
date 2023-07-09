using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<IngForEachMetricFromEachSensorSortedBySensorSerialParameters>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class IngForEachMetricFromEachSensorSortedBySensorSerialParameters : IQueryParameters
    {
        /// <summary>
        /// The number of entries per page returned. Acceptable range is 3 - 100. Default is 100. 
        /// </summary>
        public string PerPage { get; set; }
        
        /// <summary>
        /// A token used by the server to indicate the start of the page. Often this is a timestamp or an ID but 
        /// it is not limited to those. This parameter should not be defined by client applications. The link 
        /// for the first, last, prev, or next page in the HTTP Link header should define it. 
        /// </summary>
        public string StartingAfter { get; set; }
        
        /// <summary>
        /// A token used by the server to indicate the end of the page. Often this is a timestamp or an ID but 
        /// it is not limited to those. This parameter should not be defined by client applications. The link 
        /// for the first, last, prev, or next page in the HTTP Link header should define it. 
        /// </summary>
        public string EndingBefore { get; set; }
        
        /// <summary>
        /// Optional parameter to filter readings by network. 
        /// </summary>
        public string NetworkIds { get; set; }
        
        /// <summary>
        /// Optional parameter to filter readings by sensor. 
        /// </summary>
        public string Serials { get; set; }
        
        /// <summary>
        /// Types of sensor readings to retrieve. If no metrics are supplied, all available types of readings 
        /// will be retrieved. Allowed values are battery, button, door, humidity, indoorAirQuality, noise, 
        /// pm25, temperature, tvoc, and water. 
        /// </summary>
        public string Metrics { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
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
                },
                {
                    "networkIds",
                    NetworkIds
                },
                {
                    "serials",
                    Serials
                },
                {
                    "metrics",
                    Metrics
                }
            };
        }
    }
}