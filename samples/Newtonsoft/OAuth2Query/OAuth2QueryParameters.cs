using System.Collections.Generic;
namespace Postman2CSharp
{
    public class OAuth2QueryParameters : IQueryParameters
        {
            public string? AccessToken { get; set; }
            public string? RefreshToken { get; set; }
            public string? ResponseType { get; set; }
            public string? RedirectUrl { get; set; }
            public string? ClientId { get; set; }
            public string? ClientSecret { get; set; }
            public string? Code { get; set; }
            public string? GrantType { get; set; }
            public string? Scope { get; set; }
            public string? State { get; set; }
            public Dictionary<string, string?> ToDictionary()
            {
                var dictionary = new Dictionary<string, string?>();
                AddIfNotNull("access_token", AccessToken);
                AddIfNotNull("refresh_token", RefreshToken);
                AddIfNotNull("response_type", ResponseType);
                AddIfNotNull("redirect_url", RedirectUrl);
                AddIfNotNull("access_token", AccessToken);
                AddIfNotNull("grant_type", GrantType);
                AddIfNotNull("client_id", ClientId);
                AddIfNotNull("client_secret", ClientSecret);
                AddIfNotNull("code", Code);
                AddIfNotNull("scope", Scope);
                AddIfNotNull("state", State);
                return dictionary;
    
                void AddIfNotNull(string key, string? value)
                {
                    if (value != null)
                    {
                        dictionary.Add(key, value);
                    }
                }
            }
        }
}