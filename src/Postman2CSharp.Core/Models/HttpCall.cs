using System.Collections.Generic;
using Postman2CSharp.Core.Infrastructure;
using Postman2CSharp.Core.Models.PostmanCollection.Http;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Request;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Response;

namespace Postman2CSharp.Core.Models;

public class Field
{
    public required string MemberName { get; set; }
    public required string JsonMemberName { get; set; }
}

public class ClassType
{
    public required string AssignedName { get; set; }
    public required List<Field> Fields { get; set; }
}

public class HttpCall
{
    public required string Name { get; set; }
    public DataType RequestDataType { get; set; }
    public string? RequestSourceCode { get; set; }
    public required Request Request { get; set; }
    public required string? RequestClassName { get; set; }
    public required List<ClassType>? RequestTypes { get; set; }

    public required string? QueryParameterClassName { get; set; }
    public required string? QueryParameterSourceCode { get; set; }
    public required List<ClassType>? QueryParameterTypes { get; set; }

    public required string HttpClientFunction { get; set; }
    public required string? FormDataClassName { get; set; }
    public required string? FormDataSourceCode { get; set; }


    public DataType ResponseDataType { get; set; }
    public string? ResponseSourceCode { get; set; }
    public required string? ResponseClassName { get; set; }
    public required List<ClassType>? ResponseTypes { get; set; }
    public Response? SuccessResponse { get; set; }

    public required List<Header> UniqueHeaders { get; set; }

    public List<HttpCallMethodParameter> MethodParameters()
    {
        List<HttpCallMethodParameter> methodParameters = new();
        if (QueryParameterClassName != null)
        {
            var queryParameters = new HttpCallMethodParameter(HttpCallMethodParameterType.QueryParameters, QueryParameterClassName, "queryParameters");
            methodParameters.Add(queryParameters);
        }

        if (RequestClassName != null)
        {
            var request = new HttpCallMethodParameter(HttpCallMethodParameterType.Request, RequestClassName, "request");
            methodParameters.Add(request);
        }

        if (FormDataClassName != null)
        {
            var formData = new HttpCallMethodParameter(HttpCallMethodParameterType.FormData, FormDataClassName, "formData");
            methodParameters.Add(formData);
        }

        if (RequestDataType == DataType.Html)
        {
            var html = new HttpCallMethodParameter(HttpCallMethodParameterType.RawText, "string", "html");
            methodParameters.Add(html);
        }

        if (RequestDataType == DataType.Xml)
        {
            var xml = new HttpCallMethodParameter(HttpCallMethodParameterType.RawText, "string", "xml");
            methodParameters.Add(xml);
        }

        if (RequestDataType == DataType.Text)
        {
            var text = new HttpCallMethodParameter(HttpCallMethodParameterType.RawText, "string", "text");
            methodParameters.Add(text);
        }

        if (RequestDataType == DataType.Binary)
        {
            methodParameters.Add(new HttpCallMethodParameter(HttpCallMethodParameterType.Stream, "Stream", "stream"));
        }

        foreach (var path in Request.Url.Path ?? new List<Path>())
        {
            if (path.IsVariable())
            {
                var pathVariable = new HttpCallMethodParameter(HttpCallMethodParameterType.Path, "string", path.LocalVariableName);
                methodParameters.Add(pathVariable);
            }
        }
        return methodParameters;
    }
}
public record HttpCallMethodParameter(HttpCallMethodParameterType Type, string VariableType, string ParameterName)
{
    public static readonly HttpCallMethodParameter CancellationToken = new (HttpCallMethodParameterType.CancellationToken, "CancellationToken", "cancellationToken = default");
    public string LocalParameterDeclaration => VariableType + " " + ParameterName;
};

public enum HttpCallMethodParameterType
{
    QueryParameters,
    Request,
    FormData,
    RawText,
    Path,
    Stream,
    CancellationToken
}