namespace Postman2CSharp.Core.Models.Insomnia;

public enum InsomniaResourceType
{
    Request, // request
    RequestGroup, // request_group
    Workspace, // workspace
    Environment, // environment
    CookieJar // cookie_jar
}

public enum InsomniaAuthenticationType
{
    Digest, // digest,
    Netric, // netrc,
    Basic, // basic,
    OAuth1, // oauth1,
    OAuth2, // oauth2,
    Ntlm, // ntlm,
    Hawk, // hawk,
    AwsIam, // iam, v4
    BearerToken, // bearer
    NoAuth, // noauth
    Asap, // asap, AtlassianAsap
    ApiKey, // apikey
}