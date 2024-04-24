using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Postman2CSharp.Core.Infrastructure;
using Postman2CSharp.Core.Models;
using Postman2CSharp.Core.Models.PostmanCollection.Authorization;
using Postman2CSharp.Core.Models.PostmanCollection.Http;
using Postman2CSharp.Core.Utilities;

namespace Postman2CSharp.Core.Serialization;

public static class ApiClientSerializer
{
    public static string SerializeApiClient(ApiClient client)
    {
        var logsExceptions = (client.Options.ErrorHandlingStrategy != ErrorHandlingStrategy.None
                              && client.Options.ErrorHandlingSinks.Any(x => x == ErrorHandlingSinks.LogException)) 
                             || client.Options.ExecuteWithRetry;

        var sb = new StringBuilder();
        if (client.Options.XmlCommentTypes.Contains(XmlCommentTypes.ApiClient) && !string.IsNullOrWhiteSpace(client.Description))
        {
            var xmlComment = XmlCommentHelpers.ToXmlSummary(client.Description.HtmlToPlainText(), string.Empty);
            sb.Append(xmlComment);
        }
        sb.AppendLine($"public class {client.Name} : {client.InterfaceName}");
        sb.AppendLine("{");
        var indent = Consts.Indent(1);
        if (client.Options.ExecuteWithRetry)
        {
            sb.AppendLineIndented(indent, "private HttpClient _httpClient;");
        }
        else
        {
            sb.AppendLineIndented(indent, "private readonly HttpClient _httpClient;");
        }
        if (logsExceptions)
        {
            sb.AppendLineIndented(indent, $"private readonly ILogger<{client.Name}> _logger;");
        }
        VariableUsagesAsPrivateProperties(sb, client.BaseUrl ?? string.Empty, client.VariableUsages, indent);
        AuthProperties(client.CollectionAuth, sb, indent);
        foreach (var otherUniqueAuth in client.UniqueAuths.Where(x => x.EnumType() != client.CollectionAuth?.EnumType()))
        {
            AuthProperties(otherUniqueAuth, sb, indent);
        }
        ApiClientConstructor(sb, client, logsExceptions);

        if (client.UniqueAuths.FirstOrDefault(x => x.EnumType() == PostmanAuthType.oauth2) is { } uniqueAuthOAuth2)
        {
            AddOAuth2Methods(sb, uniqueAuthOAuth2, 1);
        }

        ApiClientCalls(sb, client.BaseUrl, client.HttpCalls, constructorHasAuthHeader: client.AddAuthHeaderToConstructor, client.Options, client.GraphQLQueriesClassName);
        sb.AppendLine();
        if (client.Options.ExecuteWithRetry)
        {
            sb.AppendLine();
            ExecuteWithRetryFunction(sb, 1, client.Name, client.BaseUrl ?? string.Empty, client.Options, client.CommonHeaders, client.UniqueAuths, client.AddAuthHeaderToConstructor);
        }
        sb.AppendLine("}");
        var str = sb.ToString();

        SyntaxTree tree = CSharpSyntaxTree.ParseText(str);
        var root = tree.GetRoot();
        var newRoot = CodeAnalysisUtils.CarefullyRemoveAsyncAwait(root);
        return newRoot.ToFullString();
    }

    private static void ApiClientConstructor(StringBuilder sb, ApiClient client, bool logsExceptions)
    {
        var indent = Consts.Indent(1);
        List<string> constructorParameters = new (3);
        if (client.Options.HttpClientInConstructor)
        {
            constructorParameters.Add("HttpClient httpClient");
        }
        if (logsExceptions)
        {
            constructorParameters.Add($"ILogger<{client.Name}> logger");
        }
        if (client.UniqueAuths.Any(x => x.EnumType() is PostmanAuthType.apikey or PostmanAuthType.oauth1 or PostmanAuthType.oauth2
                or PostmanAuthType.awsv4 or PostmanAuthType.bearer or PostmanAuthType.jwt or PostmanAuthType.basic))
        {
            constructorParameters.Add($"IConfiguration config");
        }
        sb.AppendLineIndented(indent, $"public {client.Name}({string.Join(", ", constructorParameters)})");
        sb.AppendLineIndented(indent, "{");
        indent = Consts.Indent(2);
        string? baseUrl = client.BaseUrl;
        if (!string.IsNullOrWhiteSpace(baseUrl) && !baseUrl.EndsWith("/"))
        {
            baseUrl += "/";
        }
        if (client.Options.HttpClientInConstructor)
        {
            sb.AppendLineIndented(indent, "_httpClient = httpClient;");
            if (baseUrl != null)
            {
                sb.AppendLineIndented(indent, $"_httpClient.BaseAddress = new Uri($\"{baseUrl}\");");
            }
        }
        else
        {
            sb.AppendLineIndented(indent, "_httpClient = new HttpClient()");
            sb.AppendLineIndented(indent, "{");
            indent = Consts.Indent(3);
            if (baseUrl != null)
            {
                sb.AppendLineIndented(indent, $"BaseAddress = new Uri($\"{baseUrl}\");");
            }
            indent = Consts.Indent(2);
            sb.AppendLineIndented(indent, "}");
        }
        if (logsExceptions)
        {
            sb.AppendLineIndented(indent, "_logger = logger;");
        }

        if (client.UniqueAuths.Count > 0)
        {
            sb.AppendLine();
        }

        if (client.AddAuthHeaderToConstructor)
        {
            var auth = client.UniqueAuths.SingleOrDefault();
            sb.AddAuthVariablesToConstructor(auth, indent);
            sb.SetAuthenticationHeader(auth, indent);
        }
        else
        {
            foreach (var unique in client.UniqueAuths)
            {
                sb.AddAuthVariablesToConstructor(unique, indent);
            }
        }

        var importantHeaders = client.CommonHeaders.Where(Header.IsImportant).ToList();
        if (importantHeaders.Count > 0)
        {
            sb.AppendLine();
        }

        foreach (var header in importantHeaders)
        {
            sb.AddDefaultRequestHeader(indent, $"$\"{header.Key}\"", $"$\"{header.Value}\"");
        }

        indent = Consts.Indent(1);
        sb.AppendLineIndented(indent, "}");
    }

    private static void ApiClientCalls(StringBuilder sb, string? baseUrl, List<HttpCall> calls, bool constructorHasAuthHeader, 
        ApiClientOptions options, string graphQlQueriesClassName)
    {
        var last = calls.Last();
        foreach (var call in calls)
        {
            sb.AppendLine();
            HttpCallSerializer.SerializeHttpCall(sb, baseUrl, call, constructorHasAuthHeader, options, graphQlQueriesClassName);
            if (!Equals(call, last))
            {
                sb.AppendLine();
            }
        }
    }

    private static void AuthProperties(AuthSettings? authSettings, StringBuilder sb, string indent)
    {
        if (authSettings == null) return;
        if (authSettings.EnumType() == PostmanAuthType.oauth2)
        {
            AddOAuth2Properties(authSettings, sb, indent);
        }
        if (authSettings.EnumType() == PostmanAuthType.apikey)
        {
            sb.AddPrivateReadonlyString(indent, Consts._apiKey);
        }
        if (authSettings.EnumType() == PostmanAuthType.bearer)
        {
            sb.AddPrivateReadonlyString(indent, Consts._bearerToken);
        }
        if (authSettings.EnumType() == PostmanAuthType.awsv4)
        {
            sb.AddPrivateReadonlyString(indent, Consts._awsSignature);
        }
        if (authSettings.EnumType() == PostmanAuthType.jwt)
        {
            sb.AddPrivateReadonlyString(indent, Consts._jwt);
        }
        if (authSettings.EnumType() == PostmanAuthType.basic)
        {
            sb.AddPrivateReadonlyString(indent, Consts._encodedCredentials);
        }
    }

    private static void VariableUsagesAsPrivateProperties(StringBuilder sb, string baseUrl, List<VariableUsage> variableUsages, string indent)
    {
        foreach (var variableUsage in variableUsages.Where(x => !x.Original.StartsWith(":")))
        {
            var normalizedKey = variableUsage.CSPropertyUsage(CsharpPropertyType.Private);
            var valueLiteral = string.IsNullOrWhiteSpace(variableUsage.Value) ? "string.Empty" : $"\"{variableUsage.Value}\"";
            sb.AppendLineIndented(indent, $"private string {normalizedKey} = {valueLiteral};");
        }

        if (baseUrl.Contains("{_unknownBaseUrl}"))
        {
            sb.AppendLineIndented(indent, $"private readonly string _unknownBaseUrl = \"NOT IN POSTMAN. FILL MANUALLY\";");
        }
    }

    private static void AddOAuth2Methods(StringBuilder sb, AuthSettings authSettings, int indent)
    {
        AddNotImplementedFunction(sb, OAuth2Functions.GetAccessToken, indent, returnType: "string");
        sb.AppendLine();
        AddNotImplementedFunction(sb, OAuth2Functions.GetRefreshToken, indent, returnType: "string");
        sb.AppendLine();
        AddNotImplementedFunction(sb, OAuth2Functions.PersistAccessToken, indent, paramaters: new List<string>() { "string accessToken" });
        sb.AppendLine();
        AddNotImplementedFunction(sb, OAuth2Functions.PersistRefreshToken, indent, paramaters: new List<string>() { "string refreshToken" });
        sb.AppendLine();
        if (authSettings.TryGetAuth2Config(OAuth2Config.AddTokenTo, out var addTokenToValue))
        {
            if (Enum.TryParse<AddTokenTo>(addTokenToValue, true, out var addTokenTo))
            {
                if (addTokenTo == AddTokenTo.Header)
                {
                    authSettings.TryGetAuth2Config(OAuth2Config.HeaderPrefix, out var headerPrefix);
                    AddAccessTokenToHeader(sb, headerPrefix, indent);
                    sb.AppendLine();
                }

                if (addTokenTo == AddTokenTo.QueryParams)
                {

                }
            }
        }
        if (authSettings.TryGetAuth2Config(OAuth2Config.GrantType, out var grantTypeValue))
        {
            grantTypeValue = grantTypeValue?.Replace("_", string.Empty);
            if (Enum.TryParse<GrantType>(grantTypeValue, true, out var grantType))
            {
                if (grantType == GrantType.AuthorizationCode)
                {
                    AuthorizationCode(sb, authSettings, indent);
                    sb.AppendLine();
                    Auth(sb, authSettings, indent);
                }

                if (grantType == GrantType.Implicit)
                {
                    Implicit(sb, authSettings, indent);
                    sb.AppendLine();
                    Auth(sb, authSettings, indent);
                }
            }
        }
        // For some reason if we don't have a grant type we can usually assume it's authorization code
        else
        {
            AuthorizationCode(sb, authSettings, indent);
            sb.AppendLine();
            Auth(sb, authSettings, indent);
        }

        sb.AppendLine();

        static void AddAccessTokenToHeader(StringBuilder sb, string? headerPrefix, int intIndent)
        {
            headerPrefix ??= "Bearer";
            var indent = Consts.Indent(intIndent);
            sb.AppendLineIndented(indent, $"private async Task {OAuth2Functions.AddAccessTokenToRequest}()");
            sb.AppendLineIndented(indent, "{");
            indent = Consts.Indent(intIndent + 1);
            var accessToken = "accessToken";
            sb.AppendLineIndented(indent, $"var {accessToken} = await {OAuth2Functions.GetAccessToken}();");
            sb.SetDefaultAuthorizationHeader(indent, $"$\"{headerPrefix}\"", accessToken);
            indent = Consts.Indent(intIndent);
            sb.AppendLineIndented(indent, "}");
        }

        static void Implicit(StringBuilder sb, AuthSettings auth, int intIndent)
        {
            var indent = Consts.Indent(intIndent);
            sb.AppendLineIndented(indent, "public async Task DoAuth()");
            sb.AppendLineIndented(indent, "{");
            indent = Consts.Indent(2);
            sb.AppendLineIndented(indent, "var oauthQueryParameters = new OAuth2QueryParameters();");
            sb.AppendLineIndented(indent, "oauthQueryParameters.ResponseType = \"token\";");
            sb.AppendLineIndented(indent, "oauthQueryParameters.ClientId = _clientId;");

            sb.AddCommonApiClientInstanceAuthVariablesToOAuthQueryParameters(auth, indent);

            auth.TryGetAuth2Config(OAuth2Config.AuthUrl, out var authUrl);
            sb.AppendLineIndented(indent, $"var queryString = QueryHelpers.AddQueryString(\"{authUrl}\", oauthQueryParameters.ToDictionary());");
            sb.AppendLineIndented(indent, $"await _httpClient.GetAsync(queryString);");
            indent = Consts.Indent(intIndent);
            sb.AppendLineIndented(indent, "}");
        }

        static void AuthorizationCode(StringBuilder sb, AuthSettings auth, int intIndent)
        {
            var indent = Consts.Indent(intIndent);
            sb.AppendLineIndented(indent, "public async Task DoAuth()");
            sb.AppendLineIndented(indent, "{");
            indent = Consts.Indent(2);
            sb.AppendLineIndented(indent, "var oauthQueryParameters = new OAuth2QueryParameters();");
            sb.AppendLineIndented(indent, "oauthQueryParameters.ResponseType = \"code\";");
            sb.AppendLineIndented(indent, "oauthQueryParameters.ClientId = _clientId;");

            sb.AddCommonApiClientInstanceAuthVariablesToOAuthQueryParameters(auth, indent);

            auth.TryGetAuth2Config(OAuth2Config.AuthUrl, out var authUrl);
            sb.AppendLineIndented(indent, $"var queryString = QueryHelpers.AddQueryString(\"{authUrl}\", oauthQueryParameters.ToDictionary());");
            sb.AppendLineIndented(indent, $"await _httpClient.GetAsync(queryString);");
            indent = Consts.Indent(intIndent);
            sb.AppendLineIndented(indent, "}");
        }
        static void Auth(StringBuilder sb, AuthSettings auth, int intIndent)
        {
            var indent = Consts.Indent(intIndent);
            sb.AppendIndented(indent, "public async Task Auth(string code");
            if (auth.TryGetAuth2Config(OAuth2Config.State, out var _))
            {
                sb.Append(", string state");
            }
            sb.AppendLine(")");
            sb.AppendLineIndented(indent, "{");
            indent = Consts.Indent(2);
            sb.AppendLineIndented(indent, $"var encoded = Convert.ToBase64String(Encoding.GetEncoding(\"ISO-8859-1\").GetBytes({OAuth2Properties.ClientId} + \":\" + {OAuth2Properties.ClientSecret}));");
            sb.AppendLineIndented(indent, "_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(\"Basic\", encoded);");
            sb.AppendLine();
            sb.AppendLineIndented(indent, "var oauthQueryParameters = new OAuth2QueryParameters();");
            sb.AppendLineIndented(indent, "oauthQueryParameters.Code = code;");
            sb.AppendLineIndented(indent, "oauthQueryParameters.GrantType = \"authorization_code\";");

            sb.AddCommonApiClientInstanceAuthVariablesToOAuthQueryParameters(auth, indent);

            auth.TryGetAuth2Config(OAuth2Config.AccessTokenUrl, out var accessTokenUrl);
            sb.AppendLineIndented(indent, $"var response = await _httpClient.PostAsync(\"{accessTokenUrl}\", new FormUrlEncodedContent(oauthQueryParameters.ToDictionary()));");
            indent = Consts.Indent(intIndent);
            sb.AppendLineIndented(indent, "}");
        }
    }

    private static void AddCommonApiClientInstanceAuthVariablesToOAuthQueryParameters(this StringBuilder sb, AuthSettings auth, string indent)
    {
        VariableOrEmpty(OAuth2Properties.RedirectUrl, OAuth2Config.RedirectUri, out var redirectUrl);
        sb.AppendLineIndented(indent, $"oauthQueryParameters.RedirectUrl = {redirectUrl};");

        if (VariableOrEmpty(OAuth2Properties.Scope, OAuth2Config.Scope, out var scope))
        {
            sb.AppendLineIndented(indent, $"oauthQueryParameters.Scope = {scope};");
        }

        if (VariableOrEmpty(OAuth2Properties.State, OAuth2Config.State, out var state))
        {
            sb.AppendLineIndented(indent, $"oauthQueryParameters.State = {state};");
        }
        bool VariableOrEmpty(string str, OAuth2Config config, out string value)
        {
            if (!auth.TryGetAuth2Config(config, out var _))
            {
                value = "string.Empty";
                return false;
            }
            value = str;
            return true;
        }
    }

    private static void AddNotImplementedFunction(StringBuilder sb, string functionName, int intIndent, List<string>? paramaters = null, string? returnType = null)
    {
        returnType = returnType == null ? "Task" : $"Task<{returnType}>";
        var indent = Consts.Indent(intIndent);
        sb.AppendLineIndented(indent, "// You must implement this yourself");
        sb.AppendIndented(indent, $"private async {returnType} {functionName}(");
        sb.AppendLine(string.Join(", ", paramaters ?? new()) + ")");
        sb.AppendLineIndented(indent, "{");
        indent = Consts.Indent(intIndent + 1);
        sb.AppendLineIndented(indent, "throw new NotImplementedException();");
        indent = Consts.Indent(intIndent);
        sb.AppendLineIndented(indent, "}");
    }

    private static void AddOAuth2Properties(AuthSettings authSettings, StringBuilder sb, string indent)
    {
        sb.AddPrivateReadonlyString(indent, OAuth2Properties.ClientId);
        sb.AddPrivateReadonlyString(indent, OAuth2Properties.ClientSecret);
        if (authSettings.TryGetAuth2Config(OAuth2Config.AccessTokenUrl, out var _))
        {
            sb.AddPrivateReadonlyString(indent, OAuth2Properties.AccessTokenUrl);
        }
        if (authSettings.TryGetAuth2Config(OAuth2Config.RedirectUri, out var _))
        {
            sb.AddPrivateReadonlyString(indent, OAuth2Properties.RedirectUrl);
        }
        if (authSettings.TryGetAuth2Config(OAuth2Config.AuthUrl, out var _))
        {
            sb.AddPrivateReadonlyString(indent, OAuth2Properties.AuthUrl);
        }
        if (authSettings.TryGetAuth2Config(OAuth2Config.RefreshTokenUrl, out var _))
        {
            sb.AddPrivateReadonlyString(indent, OAuth2Properties.RefreshTokenUrl);
        }
        if (authSettings.TryGetAuth2Config(OAuth2Config.Scope, out var _))
        {
            sb.AddPrivateReadonlyString(indent, OAuth2Properties.Scope);
        }
        if (authSettings.TryGetAuth2Config(OAuth2Config.State, out var _))
        {
            sb.AddPrivateReadonlyString(indent, OAuth2Properties.State);
        }
    }

    private static void ExecuteWithRetryFunction(StringBuilder sb, int intIndent, string apiClientName, string baseUrl, ApiClientOptions options,
        List<Header> commonHeaders, List<AuthSettings> uniqueAuths, bool addAuthToHeader)
    {
        var oneIndent = Consts.Indent(1);
        var indent = Consts.Indent(intIndent);
        sb.AppendLineIndented(indent, "private async Task<T> ExecuteWithRetry<T>(Func<Task<T>> operation, int retryCount = 2,");
        sb.AppendLineIndented(indent, $"{oneIndent}[CallerMemberName] string callerMemberName = \"\", [CallerFilePath] string sourceFilePath = \"\", [CallerLineNumber] int sourceLineNumber = 0)");
        sb.AppendLineIndented(indent, "{");
        indent = Consts.Indent(intIndent + 1);

        sb.AppendLineIndented(indent, "for (int i = 0; i < retryCount; i++)");
        sb.AppendLineIndented(indent, "{");
        indent = Consts.Indent(intIndent + 2);

        sb.AppendLineIndented(indent, "try");
        sb.AppendLineIndented(indent, "{");
        indent = Consts.Indent(intIndent + 3);
        sb.AppendLineIndented(indent, "return await operation();");
        indent = Consts.Indent(intIndent + 2);
        sb.AppendLineIndented(indent, "}");

        sb.AppendLineIndented(indent, "catch (Exception ex)");
        sb.AppendLineIndented(indent, "{");
        indent = Consts.Indent(intIndent + 3);
        sb.AppendLineIndented(indent, "if (i >= retryCount - 1)");
        sb.AppendLineIndented(indent, "{");
        indent = Consts.Indent(intIndent + 4);

        var throwDeclaration = $"throw new Exception($\"{apiClientName}: Operation failed. Retries exceeded. Allowed Retries: {{retryCount}}. Source - Member Name: {{callerMemberName}}, File Path: {{sourceFilePath}}, Line Number: {{sourceLineNumber}}\", ex);";
        sb.ErrorHandlingStrategy(throwDeclaration, options.ErrorHandlingStrategy, indent);

        indent = Consts.Indent(intIndent + 3);
        sb.AppendLineIndented(indent, "}");
        sb.AppendLine();

        sb.ErrorHandlingSinks(options.ErrorHandlingSinks, LogLevel.Information, indent, $"{apiClientName}: Operation failed. Retrying... Retry Attempt: {{i + 1}}, Allowed Retries: {{retryCount}}. Source - Member Name: {{callerMemberName}}, File Path: {{sourceFilePath}}, Line Number: {{sourceLineNumber}}");

        indent = Consts.Indent(intIndent + 2);
        sb.AppendLineIndented(indent, "}");


        indent = Consts.Indent(intIndent + 1);
        sb.AppendLineIndented(indent, "}");
        indent = Consts.Indent(intIndent);
        sb.AppendLineIndented(indent, "}");
    }
}