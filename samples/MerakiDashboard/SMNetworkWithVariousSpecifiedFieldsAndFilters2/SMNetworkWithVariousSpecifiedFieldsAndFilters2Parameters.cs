using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<SMNetworkWithVariousSpecifiedFieldsAndFilters2Parameters>(myJsonResponse);
namespace MerakiDashboard
{
    public class SMNetworkWithVariousSpecifiedFieldsAndFilters2Parameters : IQueryParameters
    {
        /// <summary>
        /// Filter users by id(s).
        /// </summary>
        public string Ids { get; set; }
        
        /// <summary>
        /// Filter users by username(s).
        /// </summary>
        public string Usernames { get; set; }
        
        /// <summary>
        /// Filter users by email(s).
        /// </summary>
        public string Emails { get; set; }
        
        /// <summary>
        /// Specifiy a scope (one of all, none, withAny, withAll, withoutAny, withoutAll) and a set of tags.
        /// </summary>
        public string Scope { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "ids",
                    Ids
                },
                {
                    "usernames",
                    Usernames
                },
                {
                    "emails",
                    Emails
                },
                {
                    "scope",
                    Scope
                }
            };
        }
    }
}