using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SystemTextJson
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<PlaceDetailsParameters>(myJsonResponse);
    public class PlaceDetailsParameters : IQueryParameters
    {
        /// <summary>
        /// (Required) A textual identifier that uniquely identifies a place, returned from a [Place 
        /// Search](https://developers.google.com/maps/documentation/places/web-service/search).
        /// For more 
        /// information about place IDs, see the [place ID 
        /// overview](https://developers.google.com/maps/documentation/places/web-service/place-id).
        ///  
        /// </summary>
        public string PlaceId { get; set; }
        
        /// <summary>
        /// > Caution: Place Search requests and Place Details requests do not return the same fields. Place 
        /// Search requests return a subset of the fields that are returned by Place Details requests. If the 
        /// field you want is not returned by Place Search, you can use Place Search to get a place_id, then use 
        /// that Place ID to make a Place Details request. For more information on the fields that are 
        /// unavailable in a Place Search request, see Places API fields support
        /// 
        /// Use the fields parameter to 
        /// specify a comma-separated list of place data types to return. For example: 
        /// `fields=formatted_address,name,geometry`. Use a forward slash when specifying compound values. For 
        /// example: `opening_hours/open_now`.
        /// 
        /// Fields are divided into three billing categories: Basic, 
        /// Contact, and Atmosphere. Basic fields are billed at base rate, and incur no additional charges. 
        /// Contact and Atmosphere fields are billed at a higher rate. See the [pricing 
        /// sheet](https://cloud.google.com/maps-platform/pricing/sheet/) for more information. Attributions, 
        /// `html_attributions`, are always returned with every call, regardless of whether the field has been 
        /// requested.
        /// 
        /// **Basic**
        /// 
        /// The Basic category includes the following fields: `address_component`, 
        /// `adr_address`, `business_status`, `formatted_address`, `geometry`, `icon`, `icon_mask_base_uri`, 
        /// `icon_background_color`, `name`, `permanently_closed` 
        /// ([deprecated](https://developers.google.com/maps/deprecations)), `photo`, `place_id`, `plus_code`, 
        /// `type`, `url`, `utc_offset`, `vicinity`.
        /// 
        /// **Contact**
        /// 
        /// The Contact category includes the following 
        /// fields: `formatted_phone_number`, `international_phone_number`, `opening_hours`, 
        /// `website`
        /// 
        /// **Atmosphere**
        /// 
        /// The Atmosphere category includes the following fields: `price_level`, 
        /// `rating`, `review`, `user_ratings_total`.
        ///  
        /// </summary>
        public string Fields { get; set; }
        
        /// <summary>
        /// A random string which identifies an autocomplete 
        /// [session](https://developers.google.com/maps/documentation/places/web-service/details#session_tokens) 
        /// for billing purposes.
        /// 
        /// The session begins when the user starts typing a query, and concludes when 
        /// they select a place and a call to Place Details is made. Each session can have multiple queries, 
        /// followed by one place selection. The API key(s) used for each request within a session must belong 
        /// to the same Google Cloud Console project. Once a session has concluded, the token is no longer 
        /// valid; your app must generate a fresh token for each session. If the `sessiontoken` parameter is 
        /// omitted, or if you reuse a session token, the session is charged as if no session token was provided 
        /// (each request is billed separately).
        /// 
        /// We recommend the following guidelines:
        /// 
        /// - Use session tokens 
        /// for all autocomplete sessions.
        /// - Generate a fresh token for each session. Using a version 4 UUID is 
        /// recommended.
        /// - Ensure that the API key(s) used for all Place Autocomplete and Place Details requests 
        /// within a session belong to the same Cloud Console project.
        /// - Be sure to pass a unique session token 
        /// for each new session. Using the same token for more than one session will result in each request 
        /// being billed individually.
        ///  
        /// </summary>
        public string Sessiontoken { get; set; }
        
        /// <summary>
        /// The language in which to return results.
        /// 
        /// * See the [list of supported 
        /// languages](https://developers.google.com/maps/faq#languagesupport). Google often updates the 
        /// supported languages, so this list may not be exhaustive.
        /// * If `language` is not supplied, the API 
        /// attempts to use the preferred language as specified in the `Accept-Language` header.
        /// * The API does 
        /// its best to provide a street address that is readable for both the user and locals. To achieve that 
        /// goal, it returns street addresses in the local language, transliterated to a script readable by the 
        /// user if necessary, observing the preferred language. All other addresses are returned in the 
        /// preferred language. Address components are all returned in the same language, which is chosen from 
        /// the first component.
        /// * If a name is not available in the preferred language, the API uses the 
        /// closest match.
        /// * The preferred language has a small influence on the set of results that the API 
        /// chooses to return, and the order in which they are returned. The geocoder interprets abbreviations 
        /// differently depending on language, such as the abbreviations for street types, or synonyms that may 
        /// be valid in one language but not in another. For example, _utca_ and _tér_ are synonyms for street 
        /// in Hungarian. 
        /// </summary>
        public string Language { get; set; }
        
        /// <summary>
        /// The region code, specified as a [ccTLD ("top-level 
        /// domain")](https://en.wikipedia.org/wiki/List_of_Internet_top-level_domains#Country_code_top-level_domains) 
        /// two-character value. Most ccTLD codes are identical to ISO 3166-1 codes, with some notable 
        /// exceptions. For example, the United Kingdom's ccTLD is "uk" (.co.uk) while its ISO 3166-1 code is 
        /// "gb" (technically for the entity of "The United Kingdom of Great Britain and Northern Ireland"). 
        /// </summary>
        public string Region { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "place_id",
                    PlaceId
                },
                {
                    "fields",
                    Fields
                },
                {
                    "sessiontoken",
                    Sessiontoken
                },
                {
                    "language",
                    Language
                },
                {
                    "region",
                    Region
                }
            };
        }
    }
}