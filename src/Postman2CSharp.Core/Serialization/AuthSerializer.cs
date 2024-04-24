using Postman2CSharp.Core.Models.PostmanCollection.Authorization;
using System;
using System.Text;
using Postman2CSharp.Core.Infrastructure;

namespace Postman2CSharp.Core.Serialization
{
    public static class AuthSerializer
    {
        public static void AddAuthVariablesToConstructor(this StringBuilder sb, AuthSettings? auth, string indent)
        {
            switch (auth?.EnumType())
            {
                case PostmanAuthType.noauth:
                    break;
                case PostmanAuthType.basic:
                    sb.AssignVariableToConfig(indent, "var username", "UserName");
                    sb.AssignVariableToConfig(indent, "var password", "Password");
                    sb.AppendLineIndented(indent, $@"{Consts._encodedCredentials} = Convert.ToBase64String(Encoding.ASCII.GetBytes($""{{username}}:{{password}}""));");
                    break;
                case PostmanAuthType.oauth1:
                    break;
                case PostmanAuthType.oauth2:
                    sb.AssignVariableToConfig(indent, OAuth2Properties.ClientId, "OAuthClientId");
                    sb.AssignVariableToConfig(indent, OAuth2Properties.ClientSecret, "OAuthClientSecret");
                    if (auth.TryGetAuth2Config(OAuth2Config.AccessTokenUrl, out var _))
                    {
                        sb.AssignVariableToConfig(indent, OAuth2Properties.AccessTokenUrl, "AccessTokenUrl");
                    }

                    if (auth.TryGetAuth2Config(OAuth2Config.RedirectUri, out var _))
                    {
                        sb.AssignVariableToConfig(indent, OAuth2Properties.RedirectUrl, "RedirectUrl");
                    }

                    if (auth.TryGetAuth2Config(OAuth2Config.AuthUrl, out var _))
                    {
                        sb.AssignVariableToConfig(indent, OAuth2Properties.AuthUrl, "AuthTokenUrl");
                    }

                    if (auth.TryGetAuth2Config(OAuth2Config.RefreshTokenUrl, out var _))
                    {
                        sb.AssignVariableToConfig(indent, OAuth2Properties.RefreshTokenUrl, "RefreshTokenUrl");
                    }

                    if (auth.TryGetAuth2Config(OAuth2Config.Scope, out var _))
                    {
                        sb.AssignVariableToConfig(indent, OAuth2Properties.Scope, "Scope");
                    }

                    if (auth.TryGetAuth2Config(OAuth2Config.State, out var _))
                    {
                        sb.AssignVariableToConfig(indent, OAuth2Properties.State, "State");
                    }
                    break;
                case PostmanAuthType.bearer:
                    sb.AssignVariableToConfig(indent, Consts._bearerToken, "BearToken");
                    break;
                case PostmanAuthType.jwt:
                    sb.AppendLineIndented(indent, "// TODO: JWT will not generally be put in your appsettings.json. Consider this a placeholder.");
                    sb.AssignVariableToConfig(indent, Consts._jwt, "Jwt");
                    break;
                case PostmanAuthType.digest:
                    break;
                case PostmanAuthType.apikey:
                    sb.AssignVariableToConfig(indent, Consts._apiKey, "ApiKey");
                    break;
                case PostmanAuthType.awsv4:
                    sb.AssignVariableToConfig(indent, Consts._awsSignature, "AwsSignature");
                    break;
                case PostmanAuthType.hawk:
                    break;
                case PostmanAuthType.ntlm:
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(auth));
            }
        }

        public static void SetAuthenticationHeader(this StringBuilder sb, AuthSettings? auth, string indent)
        {
            switch (auth?.EnumType())
            {
                case PostmanAuthType.noauth:
                    break;
                case PostmanAuthType.basic:
                    sb.SetDefaultAuthorizationHeader(indent, @"""Basic""", Consts._encodedCredentials);
                    break;
                case PostmanAuthType.oauth1:
                    break;
                case PostmanAuthType.oauth2:
                    break;
                case PostmanAuthType.bearer:
                    sb.SetDefaultAuthorizationHeader(indent, @"""Bearer""", Consts._bearerToken);
                    break;
                case PostmanAuthType.jwt:
                    if (auth.TryGetJwtConfig(JwtConfig.AddTokenTo, out var addTokenToValue))
                    {
                        if (Enum.TryParse<AddTokenTo>(addTokenToValue, true, out var enumValue))
                        {
                            if (enumValue == AddTokenTo.Header)
                            {
                                var keyValue = auth.TryGetJwtConfig(JwtConfig.QueryParamKey, out var key) ? key : "jwt";
                                sb.SetDefaultAuthorizationHeader(indent, $@"$""{keyValue}""", Consts._jwt);
                            }
                        }
                    }

                    break;
                case PostmanAuthType.digest:
                    break;
                case PostmanAuthType.apikey:
                    if (auth.TryGetApiKeyConfig(ApiKeyConfig.In, out var value))
                    {
                        if (Enum.TryParse<In>(value, true, out var enumValue))
                        {
                            if (enumValue == In.Header)
                            {
                                var keyValue = auth.TryGetApiKeyConfig(ApiKeyConfig.Key, out var key) ? key : "api_key";
                                sb.AddDefaultRequestHeader(indent, $@"$""{keyValue}""", Consts._apiKey);
                            }
                        }
                    }
                    // If "in" isn't part of auth, it's in the header by default.
                    else
                    {
                        var keyValue = auth.TryGetApiKeyConfig(ApiKeyConfig.Key, out var key) ? key : "api_key";
                        sb.AddDefaultRequestHeader(indent, $@"$""{keyValue}""", Consts._apiKey);
                    }
                    break;
                case PostmanAuthType.awsv4:
                    sb.SetDefaultAuthorizationHeader(indent, @"$""AWS4-HMAC-SHA256""", Consts._awsSignature);
                    break;
                case PostmanAuthType.hawk:
                    break;
                case PostmanAuthType.ntlm:
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(auth));
            }
        }
    }
}
