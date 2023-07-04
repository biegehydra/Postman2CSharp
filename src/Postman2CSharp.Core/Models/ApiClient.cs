using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using Postman2CSharp.Core.Infrastructure;
using Postman2CSharp.Core.Models.PostmanCollection.Authorization;
using Postman2CSharp.Core.Models.PostmanCollection.Http;
using Postman2CSharp.Core.Serialization;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core.Models;

public class ApiClient
{
    private static readonly List<string> ImplicitNamespaces = new() {"System", "System.IO", "System.Net.Http", "System.Threading.Tasks"};
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
                return _allRequestsHaveSameAuth ??= false;
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
    public string ControllerClassName => $"{NameSpace}Controller";
    public required string TestClassSourceCode { get; set; }
    private string? _controllerSourceCode;
    public required string ControllerSourceCode
    {
        get => _controllerSourceCode ??= ControllerSerializer.SerializeController(this);
        set => _controllerSourceCode = value;
    }
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
    public required bool UseCancellationTokens { get; set; }
    public required int TotalClassesGenerated { get; set; }

    private List<AuthSettings>? _uniqueAuths;
    public List<AuthSettings> UniqueAuths => _uniqueAuths ??= GetUniqueAuths();

    private List<AuthSettings> GetUniqueAuths()
    {
        var allRequestAuths = HttpCalls.Select(x => x.Request.Auth).ToList();
        if (AnyRequestInheritsAuth)
        {
            allRequestAuths.Add(CollectionAuth);
        }
        return allRequestAuths
            .Where(x => (x?.EnumType() ?? PostmanAuthType.noauth) != PostmanAuthType.noauth)
            .DistinctBy(x => x!.EnumType()).ToList()!;
    }

    private bool? _anyRequestInheritsAuth;
    private bool AnyRequestInheritsAuth => _anyRequestInheritsAuth ??= HttpCalls.Any(x => x.Request.Auth == null);

    [JsonConstructor]
    [SetsRequiredMembers]
#pragma warning disable CS8618
    public ApiClient(string name, string? description, string nameSpace, string? baseUrl, List<HttpCall> httpCalls,
#pragma warning restore CS8618
        List<Header> commonHeaders, AuthSettings? collectionAuth, List<VariableUsage> variableUsages,
        bool ensureSuccessStatusCode, List<XmlCommentTypes> commentTypes, List<CatchExceptionTypes> catchExceptionTypes, List<ErrorHandlingSinks> errorHandlingSinks,
        ErrorHandlingStrategy errorHandlingStrategy, LogLevel logLevel, JsonLibrary jsonLibrary, bool useCancellationTokens, int totalClassesGenerated)
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
        UseCancellationTokens = useCancellationTokens;
        TotalClassesGenerated = totalClassesGenerated;
    }

    public void GenerateSourceCode()
    {
        SourceCode = ApiClientSerializer.SerializeApiClient(this);
        InterfaceSourceCode = InterfaceSerializer.CreateInterface(HttpCalls, NameSpace, Name, UseCancellationTokens);
        TestClassSourceCode = TestSerializer.SerializeTestClass(this);
        ControllerSourceCode = ControllerSerializer.SerializeController(this);
    }

    public List<string> NameSpaces()
    {
        var namespaces = new List<string>(DefaultApiClientNamespaces);
        namespaces.AddRange(ImplicitNamespaces);
        if (JsonLibrary == JsonLibrary.SystemTextJson && ErrorHandlingStrategy != ErrorHandlingStrategy.None && CatchExceptionTypes.Contains(Infrastructure.CatchExceptionTypes.JsonException))
        {
            namespaces.Add("System.Text.Json");
        }
        else if (JsonLibrary == JsonLibrary.NewtonsoftJson && ErrorHandlingStrategy != ErrorHandlingStrategy.None && CatchExceptionTypes.Contains(Infrastructure.CatchExceptionTypes.JsonException))
        {
            namespaces.Add("Newtonsoft.Json");
        }

        if (ErrorHandlingStrategy != ErrorHandlingStrategy.None &&
            ErrorHandlingSinks.Contains(Infrastructure.ErrorHandlingSinks.DebugWriteLine))
        {
            namespaces.Add("System.Diagnostics");
        }
        return namespaces;
    }

    public void FixNamespaces(string oldNamespace, string newNewspace)
    {
        foreach (var httpCall in HttpCalls)
        {
            if (httpCall.ResponseClassName != null)
            {
                httpCall.ResponseSourceCode = httpCall.ResponseSourceCode?.Replace($"namespace {oldNamespace}", $"namespace {newNewspace}");
            }
            if (httpCall.RequestClassName != null)
            {
                httpCall.RequestSourceCode = httpCall.RequestSourceCode?.Replace($"namespace {oldNamespace}", $"namespace {newNewspace}");
            }
            if (httpCall.QueryParameterClassName != null)
            {
                httpCall.QueryParameterSourceCode = httpCall.QueryParameterSourceCode?.Replace($"namespace {oldNamespace}", $"namespace {newNewspace}");
            }
            if (httpCall.FormDataClassName != null)
            {
                httpCall.FormDataSourceCode = httpCall.FormDataSourceCode?.Replace($"namespace {oldNamespace}", $"namespace {newNewspace}");
            }
        }
    }

}