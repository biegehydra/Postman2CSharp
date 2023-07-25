using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ListTheSplashLoginAttemptsForANetworkParameters>(myJsonResponse);
    public class ListTheSplashLoginAttemptsForANetworkParameters : IQueryParameters
    {
        /// <summary>
        /// Only return the login attempts for the specified SSID 
        /// </summary>
        public string SsidNumber { get; set; }
        
        /// <summary>
        /// The username, email, or phone number used during login 
        /// </summary>
        public string LoginIdentifier { get; set; }
        
        /// <summary>
        /// The timespan, in seconds, for the login attempts. The period will be from [timespan] seconds ago 
        /// until now. The maximum timespan is 3 months 
        /// </summary>
        public string Timespan { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "ssidNumber",
                    SsidNumber
                },
                {
                    "loginIdentifier",
                    LoginIdentifier
                },
                {
                    "timespan",
                    Timespan
                }
            };
        }
    }
}