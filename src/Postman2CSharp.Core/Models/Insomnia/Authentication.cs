using System;
using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.Insomnia;

public class Authentication
{
    public string? Type { get; set; }

    public string? State { get; set; }

    public string? Scope { get; set; }

    public bool? Disabled { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? SignatureMethod { get; set; }

    public string? ConsumerKey { get; set; }

    public string? ConsumerSecret { get; set; }

    public string? TokenKey { get; set; }

    public string? TokenSecret { get; set; }

    public string? PrivateKey { get; set; }

    public string? Version { get; set; }

    public string? Nonce { get; set; }

    public string? Timestamp { get; set; }

    public string? Callback { get; set; }

    public string? GrantType { get; set; }

    public string? AuthorizationUrl { get; set; }

    public string? AccessTokenUrl { get; set; }

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public bool? UsePkce { get; set; }

    public string? RedirectUrl { get; set; }

    public string? AccessKeyId { get; set; }

    public string? SecretAccessKey { get; set; }

    public string? SessionToken { get; set; }

    public string? Region { get; set; }

    public string? Service { get; set; }

    public string? Algorithm { get; set; }

    public string? Id { get; set; }

    public string? Key { get; set; }

    public string? Ext { get; set; }

    public bool? ValidatePayload { get; set; }

    public string? Issuer { get; set; }

    public string? Subject { get; set; }

    public string? Audience { get; set; }

    public string? AdditionalClaims { get; set; }

    public string? KeyId { get; set; }

    public string? Value { get; set; }

    public string? AddTo { get; set; }

    public bool? UseISO88591 { get; set; }

    private InsomniaAuthenticationType? _authenticationType;
    public InsomniaAuthenticationType AuthenticationType()
    {
        return _authenticationType ??= Type switch
        {
            null => InsomniaAuthenticationType.NoAuth,
            "noauth" => InsomniaAuthenticationType.NoAuth,
            "basic" => InsomniaAuthenticationType.Basic,
            "digest" => InsomniaAuthenticationType.Digest,
            "ntlm" => InsomniaAuthenticationType.Ntlm,
            "bearer" => InsomniaAuthenticationType.BearerToken,
            "apikey" => InsomniaAuthenticationType.ApiKey,
            "netrc" => InsomniaAuthenticationType.Netric,
            "oauth1" => InsomniaAuthenticationType.OAuth1,
            "oauth2" => InsomniaAuthenticationType.OAuth2,
            "hawk" => InsomniaAuthenticationType.Hawk,
            "iam" => InsomniaAuthenticationType.AwsIam,
            "asap" => InsomniaAuthenticationType.Asap,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
