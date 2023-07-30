using System;
using System.Text;

namespace Postman2CSharp.Core.Infrastructure;

public static class Consts
{
    public const string MultipartFormData = nameof(MultipartFormData);
    public const string Postman2CSharp = nameof(Postman2CSharp);
    public const string Request = nameof(Request);
    public const string Response = nameof(Response);
    public const string FormData = nameof(FormData);
    public const string QueryParameters = nameof(QueryParameters);
    public const string Parameters = nameof(Parameters);
    public const string _encodedCredentials = nameof(_encodedCredentials);
    public const string _bearerToken = nameof(_bearerToken);
    public const string _jwt = nameof(_jwt);
    public const string _apiKey = nameof(_apiKey);
    public const string _awsSignature = nameof(_awsSignature);

    private const string OneIndent = "    ";
    private static readonly string TwoIndents = OneIndent + OneIndent;
    private static readonly string ThreeIndents = TwoIndents + OneIndent; 
    private static readonly string FourIndents = ThreeIndents + OneIndent;
    private static readonly string FiveIndents = FourIndents + OneIndent;
    private static readonly string SixIndents = FiveIndents + OneIndent;
    public static string Indent(int indents)
    {
        var sb = new StringBuilder();
        return indents switch
        {
            1 => OneIndent,
            2 => TwoIndents,
            3 => ThreeIndents,
            4 => FourIndents,
            5 => FiveIndents,
            6 => SixIndents,
            > 6 => CalculateIndent(),
            _ => throw new ArgumentOutOfRangeException(nameof(indents), indents, null)
        };

        string CalculateIndent()
        {
            for (var i = 0; i < indents; i++)
            {
                sb.Append(OneIndent);
            }
            return sb.ToString();
        }
    }

    public static class Classes
    {
        public const string FormData = nameof(FormData);
        public const string MultipartFormData = nameof(MultipartFormData);
    }

    public static class Interfaces
    {
        public const string IFormData = nameof(IFormData);
        public const string IMultipartFormData = nameof(IMultipartFormData);
        public const string IQueryParameters = nameof(IQueryParameters);
    }
}
public static class OAuth2Properties
{
    public const string AccessToken = "AccessToken";
    public const string RefreshToken = "RefreshToken";
    public const string RefreshTokenUrl = "_refreshTokenUrl";
    public const string AccessTokenUrl = "_accessTokenUrl";
    public const string RedirectUrl = "_redirectTokenUrl";
    public const string AuthUrl = "_authUrl";
    public const string ClientId = "_clientId";
    public const string ClientSecret = "_clientSecret";
    public const string Scope = "_scope";
    public const string State = "_state";
}
public static class OAuth2Functions
{
    public const string GetAccessToken = "GetAccessToken";
    public const string GetRefreshToken = "GetRefreshToken";
    public const string PersistAccessToken = "PersistAccessToken";
    public const string PersistRefreshToken = "PersistRefreshToken";
    public const string AddAccessTokenToRequest = "AddAccessTokenToRequest";
}