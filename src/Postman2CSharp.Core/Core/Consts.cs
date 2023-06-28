using System.Text;

namespace Postman2CSharp.Core.Core;

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
    public static string Indent(int indents)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < indents; i++)
        {
            sb.Append("    ");
        }
        return sb.ToString();
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