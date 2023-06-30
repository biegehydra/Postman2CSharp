using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using Postman2CSharp.Core.Models.PostmanCollection.Authorization;
using Postman2CSharp.Core.Models.PostmanCollection.Http;
using Postman2CSharp.Core.Serialization;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core.Models;

public class ApiClient
{
    private static readonly List<string> DefaultApiClientNamespaces = new() { "System.Net.Http.Headers", "System.Text" };

    private bool? _allRequestsInheritAuth;
    public bool AllRequestsInheritAuth => _allRequestsInheritAuth ??= HttpCalls.All(x => x.Request.Auth == null);
    private bool? _allRequestsHaveSameAuth;
    public bool AllRequestsHaveSameAuth
    {
        get
        {
            var firstAuth = HttpCalls.First().Request.Auth;
            if (firstAuth == null)
            {
                return false;
            }
            return _allRequestsHaveSameAuth ??= HttpCalls.All(x => x.Request.Auth != null && x.Request.Auth.EnumType() != PostmanAuthType.noauth && x.Request.Auth.EnumType() == firstAuth.EnumType());
        }
    }

    public string Id { get; init; } = Guid.NewGuid().ToString();
    public required string NameSpace { get; set; }
    public required string Name { get; set; }
    public required string? Description { get; set; }
    public string InterfaceName => $"I{Name}";
    public string TestClassName => $"{Name}Tests";
    public required string TestClassSourceCode { get; set; }
    public required string? BaseUrl { get; set; }
    public required List<HttpCall> HttpCalls { get; set; }
    public required List<Header> CommonHeaders { get; set; }
    public required AuthSettings? CollectionAuth { get; set; }
    public required string SourceCode { get; set; }
    public required string InterfaceSourceCode { get; set; }
    public required List<VariableUsage> VariableUsages { get; set; }
    public required ErrorHandlingStrategy ErrorHandlingStrategy { get; set; }
    public required List<ErrorHandlingSinks> ErrorHandlingSinks { get; set; }
    public required List<CatchExceptionTypes> CatchExceptionTypes { get; set; }
    public required LogLevel LogLevel { get; set; }
    public required JsonLibrary JsonLibrary { get; set; }
    public required bool EnsureSuccessStatusCode { get; set; }
    public required List<XmlCommentTypes> CommentTypes { get; set; }

    private List<AuthSettings>? _uniqueAuths;
    public List<AuthSettings> UniqueAuths => _uniqueAuths ??= HttpCalls.Select(x => x.Request.Auth)
        .Where(x => (x?.EnumType() ?? PostmanAuthType.noauth) != PostmanAuthType.noauth)
        .DistinctBy(x => x!.EnumType()).ToList()!;

    [JsonConstructor]
    [SetsRequiredMembers]
#pragma warning disable CS8618
    public ApiClient(string name, string? description, string nameSpace, string? baseUrl, List<HttpCall> httpCalls,
#pragma warning restore CS8618
        List<Header> commonHeaders, AuthSettings? collectionAuth, List<VariableUsage> variableUsages,
        bool ensureSuccessStatusCode, List<XmlCommentTypes> commentTypes, List<CatchExceptionTypes> catchExceptionTypes, List<ErrorHandlingSinks> errorHandlingSinks,
        ErrorHandlingStrategy errorHandlingStrategy, LogLevel logLevel, JsonLibrary jsonLibrary)
    {
        Name = name;
        Description = description;
        NameSpace = nameSpace;
        BaseUrl = baseUrl;
        HttpCalls = httpCalls;
        CommonHeaders = commonHeaders;
        CollectionAuth = collectionAuth;
        VariableUsages = variableUsages;
        EnsureSuccessStatusCode = ensureSuccessStatusCode;
        CommentTypes = commentTypes;
        CatchExceptionTypes = catchExceptionTypes;
        ErrorHandlingSinks = errorHandlingSinks;
        ErrorHandlingStrategy = errorHandlingStrategy;
        LogLevel = logLevel;
        JsonLibrary = jsonLibrary;
    }

    public void GenerateSourceCode()
    {
        SourceCode = ApiClientSerializer.SerializeApiClient(this);
        InterfaceSourceCode = InterfaceSerializer.CreateInterface(HttpCalls, NameSpace, Name);
        TestClassSourceCode = TestSerializer.SerializeTestClass(this);
    }

    public List<string> NameSpaces()
    {
        var namespaces = new List<string>(DefaultApiClientNamespaces);
        if (HttpCalls.Any(x => x.HttpClientFunction.ToLower().Contains("asjson")))
        {
            namespaces.Add("System.Net.Http.Json");
        }
        return namespaces;
    }

    public void FixNamespaces(string oldNamespace, string newNewspace)
    {
        InterfaceSourceCode = InterfaceSourceCode.Replace($"namespace {oldNamespace}", $"namespace {newNewspace}");
        TestClassSourceCode = TestClassSourceCode.Replace($"namespace {oldNamespace}", $"namespace {newNewspace}");
        foreach (var httpCall in HttpCalls)
        {
            if (httpCall.ResponseClassName != null)
            {
                httpCall.ResponseSourceCode = httpCall.ResponseSourceCode!.Replace($"namespace {oldNamespace}", $"namespace {newNewspace}");
            }
            if (httpCall.RequestClassName != null)
            {
                httpCall.RequestSourceCode = httpCall.RequestSourceCode!.Replace($"namespace {oldNamespace}", $"namespace {newNewspace}");
            }
            if (httpCall.QueryParameterClassName != null)
            {
                httpCall.QueryParameterSourceCode = httpCall.QueryParameterSourceCode!.Replace($"namespace {oldNamespace}", $"namespace {newNewspace}");
            }
            if (httpCall.FormDataClassName != null)
            {
                httpCall.FormDataSourceCode = httpCall.FormDataSourceCode!.Replace($"namespace {oldNamespace}", $"namespace {newNewspace}");
            }
        }
    }

}