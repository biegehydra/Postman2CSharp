using Postman2CSharp.Core.Infrastructure;
using System.Text;
using Postman2CSharp.Core;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.UI.Infrastructure;

public static class StepperExamples
{
    public static string Json(ApiClientOptions options)
    {
        string intProp = options.AlwaysUseNullables
            ? $"{(options.AttributeUsage is JsonPropertyAttributeUsage.Never or JsonPropertyAttributeUsage.OnlyWhenNecessary  ? null : $"{Consts.Indent(1)}[JsonPropertyName(\"SomeInt\")]\n")}    public int? SomeInt {{ get; set; }}"
            : $"{(options.AttributeUsage is JsonPropertyAttributeUsage.Never or JsonPropertyAttributeUsage.OnlyWhenNecessary  ? null : $"{Consts.Indent(1)}[JsonPropertyName(\"SomeInt\")]\n")}    public int SomeInt {{ get; set; }}";
        if (options.JsonLibrary == JsonLibrary.SystemTextJson)
        {
            return
$@"public class Example
{{{(options.AttributeUsage is JsonPropertyAttributeUsage.Never or JsonPropertyAttributeUsage.OnlyWhenNecessary  ? null : $"\n{Consts.Indent(1)}[JsonPropertyName(\"PropertyName\")]")}
    public string PropertyName {{ get; set; }}{(options.AttributeUsage is JsonPropertyAttributeUsage.Never ? null : $"\n{Consts.Indent(1)}[JsonPropertyName(\"class\")]")}
    public string @class {{ get; set; }}
{intProp}
}}

public static async Task<T> ReadJsonAsync<T>(this HttpResponseMessage response, JsonSerializerOptions? jsonSerializerOptions = null)
{{
    var stringContent = await response.Content.ReadAsStringAsync();
    return JsonSerializer.Deserialize<T>(stringContent, jsonSerializerOptions ?? JsonSerializerOptions)!;
}}";
        }

        string? nullValueHandling = options.NullValueHandlingIgnore ? ", NullValueHandling = NullValueHandling.Ignore" : null;
        return
$@"public class Example
{{{(options.AttributeUsage is JsonPropertyAttributeUsage.Never or JsonPropertyAttributeUsage.OnlyWhenNecessary ? null : $"\n{Consts.Indent(1)}[JsonProperty(\"PropertyName\"{nullValueHandling})]")}
    public string PropertyName {{ get; set; }}{(options.AttributeUsage is JsonPropertyAttributeUsage.Never ? null : $"\n{Consts.Indent(1)}[JsonProperty(\"class\"{nullValueHandling})]")}
    public string @class {{ get; set; }}
{intProp}
}}

public static async Task<T> ReadNewtonsoftJsonAsync<T>(this HttpResponseMessage response,
    JsonSerializerSettings serializerSettings = null)
{{
    var stringContent = await response.Content.ReadAsStringAsync();
    return JsonConvert.DeserializeObject<T>(stringContent, serializerSettings ?? _jsonSerializerSettings);
}}";
    }

    public static string MultipleResponses(ApiClientOptions options)
    {
        var typeParam = options.MultipleResponseHandling switch
        {
            MultipleResponseHandling.OneOf => "OneOf<SearchUsersOKResponse, SearchUsersUnauthorizedResponse, SearchUsersForbiddenResponse, SearchUsersInternalServerErrorResponse>",
            MultipleResponseHandling.Object => "object",
            _ => throw new Exception("Invalid MultipleResponseHandling")
        };
        if (options.HandleMultipleResponses)
        {
            return 
$@"public async Task<{typeParam}> SearchUsers(SearchUsersParameters queryParameters)
{{
    var parametersDict = queryParameters.ToDictionary();
    var queryString = QueryHelpers.AddQueryString($""{{_version}}/users"", parametersDict);
    var response = await _httpClient.GetAsync(queryString);
    if (response.StatusCode == HttpStatusCode.OK)
    {{
        return await response.ReadJsonAsync<SearchUsersOKResponse>();
    }}
    else if (response.StatusCode == HttpStatusCode.Unauthorized)
    {{
        return await response.ReadJsonAsync<SearchUsersUnauthorizedResponse>();
    }}
    else if (response.StatusCode == HttpStatusCode.Forbidden)
    {{
        return await response.ReadJsonAsync<SearchUsersForbiddenResponse>();
    }}
        else if (response.StatusCode == HttpStatusCode.InternalServerError)
    {{
        return await response.ReadJsonAsync<SearchUsersInternalServerErrorResponse>();
    }}
    else
    {{
        throw new Exception($""SearchUsers: Unexpected response. Status Code: {{response.StatusCode}}. Content: {{await response.Content.ReadAsStringAsync()}}"");
    }}
}}";
        }
        else
        {
            return 
@"public async Task<SearchUsersResponse> SearchUsers(SearchUsersParameters queryParameters)
{
    var parametersDict = queryParameters.ToDictionary();
    return await _httpClient.GetFromJsonAsync<SearchUsersResponse>($""{_version}/users"", parametersDict);
}";
        }
    }

    public static string ErrorHandling(ApiClientOptions options)
    {
        if (options.ErrorHandlingStrategy == ErrorHandlingStrategy.None)
        {
            return 
@"public async Task<Users> GetUserById(string id)
{
    return await _httpClient.GetFromJsonAsync<Users>($""{_version}/users/{id}"");
}";
        }
        var str = 
@"public async Task<Users> GetUserById(string id)
{
    try
    {
        return await _httpClient.GetFromJsonAsync<Users>($""{_version}/users/{id}"");
    }
";
        var catches = options.CatchExceptionTypes.Select(Catch);
        string Catch(CatchExceptionTypes catchType)
        {
            var sb = new StringBuilder();
            sb.AppendLine(Consts.Indent(1) + $"catch({catchType} ex)");
            sb.AppendLine(Consts.Indent(1) + "{");
            sb.ErrorHandlingSinks(options.ErrorHandlingSinks, options.LogLevel, Consts.Indent(2));
            sb.ErrorHandlingStrategy("throw;", options.ErrorHandlingStrategy, Consts.Indent(2));
            sb.AppendLine(Consts.Indent(1) + "}");
            return sb.ToString();
        }
        str += string.Join(string.Empty, catches) + "}";
        return str;
    }

    public static string FunctionConfigurations(ApiClientOptions options)
    {
        var comment = options.MakePathCollectionVariablesFunctionParameters
            ? "// No private Variable"
            : "private readonly string _somePathCollectionVariable; // Private variable";
        var secondComment = options.MakePathCollectionVariablesFunctionParameters
            ? "// Now a function parameter"
            : "";
        var variableName = options.MakePathCollectionVariablesFunctionParameters ? "somePathCollectionVariable" : "_somePathCollectionVariable";
        string parameters = "";
        if (options.MakePathCollectionVariablesFunctionParameters)
        {
            parameters = "string somePathCollectionVariable";
            if (options.UseCancellationTokens)
            {
                parameters += ", CancellationToken cancellationToken";
            }
        }
        else
        {
            if (options.UseCancellationTokens)
            {
                parameters = "CancellationToken cancellationToken = default";
            }
        }

        var cancellationTokenValue = options.UseCancellationTokens ? ", cancellationToken: cancellationToken" : null;
        var functionCall = options.ExecuteWithRetry
            ? $@"{Consts.Indent(1)}return await ExecuteWithRetry(() => _httpClient.GetStreamAsync($""{{{variableName}}}/ticket.php""{cancellationTokenValue}));"
            : $"{Consts.Indent(1)}return await _httpClient.GetStreamAsync($\"{{{variableName}}}/ticket.php\"{cancellationTokenValue});";
        
        var executeWithRetryFunction = options.ExecuteWithRetry 
            ? @"

private async Task<T> ExecuteWithRetry<T>(Func<Task<T>> operation, int retryCount = 2,
    [CallerMemberName] string callerMemberName = """", [CallerFilePath] string sourceFilePath = """", [CallerLineNumber] int sourceLineNumber = 0)
{
    // Logic for retrying on error
    // Tip: Replace with your own retry logic using a Poly pipeline/policy
}" 
            : null;

        return $@" // Url in postman ""https://{{{{base_url}}}}/{{{{somePathCollectionVariable}}}}/ticket.php""
{comment}
public async Task<Stream> GetTickets({parameters}) {secondComment}
{{
{functionCall}
}}{executeWithRetryFunction}";
    }

    public static string ClassDeduping(ApiClientOptions options)
    {
        var getUserReturnType = "GetUserResponse";
        var postUserReturnType = "PostUserResponse";
        var patchUserReturnType = "PatchUserResponse";
        if (options.DuplicateOptions.RemoveDuplicateRoots)
        {
            postUserReturnType = getUserReturnType;
            if (options.DuplicateOptions is { RemoveSemiDuplicates: true, DifferentOriginalNameSensitivity: > 1 })
            {
                patchUserReturnType = getUserReturnType;
            }
        }

        return $@"// GetUserById and PostUser both return the exact same json.
// PatchUser returns extremely similar json but with 2 fewer properties.
// ""Same Json Name Sensitivity"" has NO effect in this scenario - Json roots
// all have different json names during deduping (even though they are roots
// and technically have no name).
public async Task<{getUserReturnType}> GetUserById(string id)
{{
    return await _httpClient.GetFromJsonAsync<Users>($""{{_version}}/users/{{id}}"");
}}

public async Task<{postUserReturnType}> PostUser(User user)
{{
    return await _httpClient.GetJsonAsync<Users>($""{{_version}}/users"", user);
}}

public async Task<{patchUserReturnType}> PatchUser(User user)
{{
    return await _httpClient.PatchJsonAsync<Users>($""{{_version}}/users"", user);
}}";
    }

    public static string Miscellaneous(ApiClientOptions options)
    {
        var constructorParameters = options.HttpClientInConstructor
            ? "HttpClient httpClient, IConfiguration config"
            : "IConfiguration config";

        var httpClientInitialization = !options.HttpClientInConstructor
            ? $"{Consts.Indent(2)}_httpClient = new HttpClient() \n" +
              $"{Consts.Indent(2)}{{\n" +
              $"{Consts.Indent(3)}BaseAddress = new Uri($\"https://someurl.com/api/\")\n" +
              $"{Consts.Indent(2)}}}"
            : $"{Consts.Indent(2)}_httpClient = httpClient;\n" +
              $"{Consts.Indent(2)}_httpClient.BaseAddress = new Uri($\"https://someurl.com/api/\");";

        var apiClientComment = options.XmlCommentTypes.Contains(XmlCommentTypes.ApiClient)
            ? $@"/// <summary>
/// Comment from root item in postman.
/// </summary>
"
            : null;
        var requestComment = options.XmlCommentTypes.Contains(XmlCommentTypes.Request)
            ? $@"
{Consts.Indent(1)}/// <summary>
{Consts.Indent(1)}/// Comment from postman request.
{Consts.Indent(1)}/// </summary>"
            : null;

        var pathVariableComment = options.XmlCommentTypes.Contains(XmlCommentTypes.PathVariables)
            ? $"\n{Consts.Indent(1)}/// <param name=\"id\">Comment from path variable.</param>"
            : null;

        var parameters = "$\"{id}\"";
        string? headersDeclaration = null;
        if (!options.RemoveDisabled.Contains(RemoveDisabled.Headers))
        {
            parameters += ", headers";
            headersDeclaration = $@"
{Consts.Indent(2)}var headers = new Dictionary<string, string>();
{Consts.Indent(2)}{{
{Consts.Indent(3)}{{ $""DisabledHeader"", $""value"" }}
{Consts.Indent(2)}}};
";
        }

        var formDataComment = options.XmlCommentTypes.Contains(XmlCommentTypes.FormData)
            ? $@"
{Consts.Indent(1)}/// <summary>
{Consts.Indent(1)}/// Comment on individual form data item.
{Consts.Indent(1)}/// </summary>"
            : null;
        var disabledFormDataVariable = !options.RemoveDisabled.Contains(RemoveDisabled.FormData)
            ? $"\n{Consts.Indent(1)}public string DisabledFormData {{get; set; }}\n"
            : null;

        var kvpInitializer = "new(\"EnabledFormData\", EnabledFormData)";
        if (options.RemoveDisabled.Contains(RemoveDisabled.FormData) == false)
        {
            kvpInitializer += ", new(\"DisabledFormData\", DisabledFormData)";
        }

        var queryParametersComment = options.XmlCommentTypes.Contains(XmlCommentTypes.QueryParameters)
            ? $@"
{Consts.Indent(1)}/// <summary>
{Consts.Indent(1)}/// Comment on individual query parameter.
{Consts.Indent(1)}/// </summary>"
            : null;
        var disabledQueryParameterVariable = !options.RemoveDisabled.Contains(RemoveDisabled.QueryParameters)
            ? $"\n{Consts.Indent(1)}public string DisabledQueryParameter {{get; set; }}\n"
            : null;

        return
            $@"{apiClientComment}public class ExampleApiClient : IExampleApiClient
{{
    private readonly HttpClient _httpClient
    public ExampleApiClient({constructorParameters})
    {{
{httpClientInitialization}
    }}
{requestComment}{pathVariableComment}
    public async Task<SearchUsersResponse> GetUsers(string id)
    {{{headersDeclaration}
        return await _httpClient.GetFromJsonAsync<SearchUsersResponse>({parameters});
    }}
}}

public class SomeFormData : IFormData
{{{formDataComment}
    public string EnabledFormData {{ get; set; }}{disabledFormDataVariable}
    public FormUrlEncodedContent ToFormData()
    {{
        return new FormUrlEncodedContent(new KeyValuePair<string, string>[] {{ {kvpInitializer} }});
    }}
}}

public class SomeQueryParameters : IQueryParameters
{{{queryParametersComment}
    public string EnabledQueryParameter {{ get; set; }}{disabledQueryParameterVariable}
}}";
    }
}