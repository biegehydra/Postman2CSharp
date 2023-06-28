using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.PostmanCollection.Authorization
{
    public enum PostmanAuthType
    {
        noauth,
        basic,
        oauth1,
        oauth2,
        bearer,
        jwt,
        digest,
        apikey,
        awsv4,
        hawk,
        ntlm
    }

    public class AuthSettings
    {
        [JsonRequired] public required string Type { get; set; }
        private PostmanAuthType? _enumType;
        public PostmanAuthType EnumType() => _enumType ??= Enum.Parse<PostmanAuthType>(Type);
        public List<KeyValueTypeTrio>? Apikey { get; set; }
        public List<KeyValueTypeTrio>? Oauth2 { get; set; }
        public List<KeyValueTypeTrio>? Jwt { get; set; }


        public bool TryGetAuth2Config(OAuth2Config config, out string? value)
        {
            value = null;
            if (Oauth2 is not {Count: > 0}) return false;
            foreach (var keyValueTypeTrio in Oauth2)
            {
                var key = keyValueTypeTrio.Key.Replace("_", string.Empty);
                if (Enum.TryParse<OAuth2Config>(key, true, out var enumValue) && enumValue == config)
                {
                    value = keyValueTypeTrio.Type switch
                    {
                        "boolean" => keyValueTypeTrio.Value.GetBoolean().ToString(),
                        "string" => keyValueTypeTrio.Value.GetString(),
                        null => keyValueTypeTrio.Value.GetString(),
#pragma warning disable CA2208
                        _ => throw new ArgumentException(@"Unhandled oauth config type.",
                            nameof(keyValueTypeTrio.Value))
#pragma warning restore CA2208
                    };
                    return true;
                }
            }

            return false;
        }

        public void SetAuth2ConfigValue(OAuth2Config config, string value)
        {
            if (Oauth2 is not {Count: > 0}) return;
            foreach (var keyValueTypeTrio in Oauth2)
            {
                var key = keyValueTypeTrio.Key.Replace("_", string.Empty);
                if (Enum.TryParse<OAuth2Config>(key, true, out var enumValue) && enumValue == config)
                {
                    var newJsonValue = $"\"{value}\"";
                    var test1 = keyValueTypeTrio.Value.GetString();
                    keyValueTypeTrio.Value = JsonDocument.Parse(newJsonValue).RootElement;
                    var test = keyValueTypeTrio.Value.GetString();
                    break;
                }
            }
        }

        public bool TryGetApiKeyConfig(ApiKeyConfig config, out string? value)
        {
            value = null;
            if (Apikey is not { Count: > 0 }) return false;
            foreach (var keyValueTypeTrio in Apikey)
            {
                var key = keyValueTypeTrio.Key.Replace("_", string.Empty);
                if (Enum.TryParse<ApiKeyConfig>(key, true, out var enumValue) && enumValue == config)
                {
                    value = keyValueTypeTrio.Type switch
                    {
                        "boolean" => keyValueTypeTrio.Value.GetBoolean().ToString(),
                        "string" => keyValueTypeTrio.Value.GetString(),
                        null => keyValueTypeTrio.Value.GetString(),
#pragma warning disable CA2208
                        _ => throw new ArgumentException(@"Unhandled oauth config type.",
                            nameof(keyValueTypeTrio.Value))
#pragma warning restore CA2208
                    };
                    return true;
                }
            }

            return false;
        }

        public bool TryGetJwtConfig(JwtConfig config, out string? value)
        {
            value = null;
            if (Jwt is not { Count: > 0 }) return false;
            foreach (var keyValueTypeTrio in Jwt)
            {
                var key = keyValueTypeTrio.Key.Replace("_", string.Empty);
                if (Enum.TryParse<JwtConfig>(key, true, out var enumValue) && enumValue == config)
                {
                    value = keyValueTypeTrio.Type switch
                    {
                        "boolean" => keyValueTypeTrio.Value.GetBoolean().ToString(),
                        "string" => keyValueTypeTrio.Value.GetString(),
                        null => keyValueTypeTrio.Value.GetString(),
#pragma warning disable CA2208
                        _ => throw new ArgumentException(@"Unhandled oauth config type.",
                            nameof(keyValueTypeTrio.Value))
#pragma warning restore CA2208
                    };
                    return true;
                }
            }

            return false;
        }
    }

    public enum JwtConfig
    {
        Algorithm,
        IsSecretBase64Encoded,
        Payload,
        AddTokenTo,
        HeaderPrefix,
        QueryParamKey,
        Header
    }

    public enum ApiKeyConfig
    {
        Key,
        Value,
        In
    }

    public enum In
    {
        Header,
        Query
    }

    public enum ClientAuthentication
    {
        Header,
        Body
    }

    public enum GrantType
    {
        AuthorizationCode,
        Implicit
    }

    public enum AddTokenTo
    {
        Header,
        QueryParams
    }

    public class OAuth2Properties
    {
        public const string AccessToken = "AccessToken";
        public const string RefreshToken = "RefreshToken";
        public const string RefreshTokenUrl = "_refreshTokenUrl";
        public const string AccessTokenUrl = "_accessTokenUrl";
        public const string RedirectUrl = "_redirectTokenUrl";
        public const string AuthUrl = "_authUrl";
        public const string ClientId = "_clientId";
        public const string ClientSecret = "_clientSecret";
        public const string Scope = "_state";
        public const string State = "_scope";
    }
    public class OAuth2Functions
    {
        public const string GetAccessToken = "GetAccessToken";
        public const string GetRefreshToken = "GetRefreshToken";
        public const string PersistAccessToken = "PersistAccessToken";
        public const string PersistRefreshToken = "PersistRefreshToken";
        public const string AddAccessTokenToRequest = "AddAccessTokenToRequest";
    }

    public enum OAuth2Config
    {
        RefreshTokenUrl,
        ClientAuthentication,
        State,
        Scope,
        ClientId,
        ClientSecret,
        AccessTokenUrl,
        GrantType,
        AuthUrl,
        RedirectUri,
        UseBrowser,
        TokenName,
        AddTokenTo,
        HeaderPrefix
    }

    public class KeyValueTypeTrio
    {
        [JsonRequired] public required string Type { get; set; }
        [JsonRequired] public required string Key { get; set; }
        [JsonRequired] public required JsonElement Value { get; set; } 
    }

    public class KeyValueTypeDescription
    {
        [JsonRequired] public required string Key { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
        public string? Description { get; set; }
    }
}