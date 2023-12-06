using System;
using System.Collections.Generic;
using System.Text.Json;

namespace GooglePlace
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AutocompleteParameters>(myJsonResponse);
    public class AutocompleteParameters : IQueryParameters
    {
        /// <summary>
        /// (Required) The text string on which to search. The Place Autocomplete service will return candidate 
        /// matches based on this string and order results based on their perceived relevance.
        /// </summary>
        public string Input { get; set; }
        
        /// <summary>
        /// A random string which identifies an autocomplete [session](https://developers.google.com/maps/documentation/places/web-service/details#session_tokens) 
        /// for billing purposes.
        /// The session begins when the user starts typing a query, and concludes when they select a place and a 
        /// call to Place Details is made. Each session can have multiple queries, followed by one place selection. 
        /// The API key(s) used for each request within a session must belong to the same Google Cloud Console project. 
        /// Once a session has concluded, the token is no longer valid; your app must generate a fresh token for 
        /// each session. If the `sessiontoken` parameter is omitted, or if you reuse a session token, the session 
        /// is charged as if no session token was provided (each request is billed separately).
        /// We recommend the following guidelines:
        /// - Use session tokens for all autocomplete sessions.
        /// - Generate a fresh token for each session. Using a version 4 UUID is recommended.
        /// - Ensure that the API key(s) used for all Place Autocomplete and Place Details requests within a session 
        /// belong to the same Cloud Console project.
        /// - Be sure to pass a unique session token for each new session. Using the same token for more than one 
        /// session will result in each request being billed individually.
        /// </summary>
        public string Sessiontoken { get; set; }
        
        /// <summary>
        /// A grouping of places to which you would like to restrict your results. Currently, you can use components 
        /// to filter by up to 5 countries. Countries must be passed as a two character, ISO 3166-1 Alpha-2 compatible 
        /// country code. For example: `components=country:fr` would restrict your results to places within France. 
        /// Multiple countries must be passed as multiple `country:XX` filters, with the pipe character `|` as a 
        /// separator. For example: `components=country:us|country:pr|country:vi|country:gu|country:mp` would restrict 
        /// your results to places within the United States and its unincorporated organized territories.>Note: 
        /// If you receive unexpected results with a country code, verify that you are using a code which includes 
        /// the countries, dependent territories, and special areas of geographical interest you intend.  You can 
        /// find code information at Wikipedia: List of ISO 3166 country codes or the ISO Online Browsing Platform
        /// </summary>
        public string Components { get; set; }
        
        /// <summary>
        /// Returns only those places that are strictly within the region defined by `location` and `radius`. This 
        /// is a restriction, rather than a bias, meaning that results outside this region will not be returned 
        /// even if they match the user input.
        /// </summary>
        public string Strictbounds { get; set; }
        
        /// <summary>
        /// The position, in the input term, of the last character that the service uses to match predictions. For 
        /// example, if the input is `Google` and the offset is 3, the service will match on `Goo`. The string determined 
        /// by the offset is matched against the first word in the input term only. For example, if the input term 
        /// is `Google abc` and the offset is 3, the service will attempt to match against `Goo abc`. If no offset 
        /// is supplied, the service will use the whole term. The offset should generally be set to the position 
        /// of the text caret.
        /// </summary>
        public string Offset { get; set; }
        
        /// <summary>
        /// The origin point from which to calculate straight-line distance to the destination (returned as `distance_meters`). 
        /// If this value is omitted, straight-line distance will not be returned. Must be specified as `latitude,longitude`.
        /// </summary>
        public string Origin { get; set; }
        
        /// <summary>
        /// The point around which to retrieve place information. This must be specified as `latitude,longitude`. 
        /// >When using the Text Search API, the `location` parameter may be overriden if the `query` contains an 
        /// explicit location such as `Market in Barcelona`.
        /// </summary>
        public string Location { get; set; }
        
        /// <summary>
        /// Defines the distance (in meters) within which to return place results. You may bias results to a specified 
        /// circle by passing a `location` and a `radius` parameter. Doing so instructs the Places service to _prefer_ 
        /// showing results within that circle; results outside of the defined area may still be displayed.
        /// The radius will automatically be clamped to a maximum value depending on the type of search and other 
        /// parameters.
        /// * Autocomplete: 50,000 meters
        /// * Nearby Search: 
        ///   * with `keyword` or `name`: 50,000 meters
        ///   * without `keyword` or `name`
        ///     * Up to 50,000 meters, adjusted dynamically based on area density, independent of `rankby` parameter.
        ///     * When using `rankby=distance`, the radius parameter will not be accepted, and will result in an 
        /// `INVALID_REQUEST`.
        /// * Query Autocomplete: 50,000 meters
        /// * Text Search: 50,000 meters
        /// </summary>
        public string Radius { get; set; }
        
        /// <summary>
        /// You can restrict results from a Place Autocomplete request to be of a certain type by passing the `types` 
        /// parameter. This parameter specifies a type or a type collection, as listed in [Place Types](/maps/documentation/places/web-service/supported_types). 
        /// If nothing is specified, all types are returned.
        /// For the value of the `types` parameter you can specify either:
        /// * Up to five values from [Table 1](/maps/documentation/places/web-service/supported_types#table1) or 
        /// [Table 2](/maps/documentation/places/web-service/supported_types#table2). For multiple values, separate 
        /// each value with a `|` (vertical bar). For example:
        ///   `types=book_store|cafe`
        /// * Any supported filter in [Table 3](/maps/documentation/places/web-service/supported_types#table3). 
        /// You can safely mix the `geocode` and `establishment` types. You cannot mix type collections (`address`, 
        /// `(cities)` or `(regions)`) with any other type, or an error occurs.
        /// The request will be rejected with an `INVALID_REQUEST` error if:
        /// * More than five types are specified.
        /// * Any unrecognized types are present.
        /// * Any types from in [Table 1](/maps/documentation/places/web-service/supported_types#table1) or [Table 
        /// 2](/maps/documentation/places/web-service/supported_types#table2) are mixed with any of the filters 
        /// in [Table 3](/maps/documentation/places/web-service/supported_types#table3).
        /// </summary>
        public string Types { get; set; }
        
        /// <summary>
        /// The language in which to return results.
        /// * See the [list of supported languages](https://developers.google.com/maps/faq#languagesupport). Google 
        /// often updates the supported languages, so this list may not be exhaustive.
        /// * If `language` is not supplied, the API attempts to use the preferred language as specified in the 
        /// `Accept-Language` header.
        /// * The API does its best to provide a street address that is readable for both the user and locals. To 
        /// achieve that goal, it returns street addresses in the local language, transliterated to a script readable 
        /// by the user if necessary, observing the preferred language. All other addresses are returned in the 
        /// preferred language. Address components are all returned in the same language, which is chosen from the 
        /// first component.
        /// * If a name is not available in the preferred language, the API uses the closest match.
        /// * The preferred language has a small influence on the set of results that the API chooses to return, 
        /// and the order in which they are returned. The geocoder interprets abbreviations differently depending 
        /// on language, such as the abbreviations for street types, or synonyms that may be valid in one language 
        /// but not in another. For example, _utca_ and _tér_ are synonyms for street in Hungarian.
        /// </summary>
        public string Language { get; set; }
        
        /// <summary>
        /// The region code, specified as a [ccTLD ("top-level domain")](https://en.wikipedia.org/wiki/List_of_Internet_top-level_domains#Country_code_top-level_domains) 
        /// two-character value. Most ccTLD codes are identical to ISO 3166-1 codes, with some notable exceptions. 
        /// For example, the United Kingdom's ccTLD is "uk" (.co.uk) while its ISO 3166-1 code is "gb" (technically 
        /// for the entity of "The United Kingdom of Great Britain and Northern Ireland").
        /// </summary>
        public string Region { get; set; }

        public Dictionary<string, string?> ToDictionary()
        {
            return new Dictionary<string, string?>
            {
                {
                    "input",
                    Input
                },
                {
                    "sessiontoken",
                    Sessiontoken
                },
                {
                    "components",
                    Components
                },
                {
                    "strictbounds",
                    Strictbounds
                },
                {
                    "offset",
                    Offset
                },
                {
                    "origin",
                    Origin
                },
                {
                    "location",
                    Location
                },
                {
                    "radius",
                    Radius
                },
                {
                    "types",
                    Types
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