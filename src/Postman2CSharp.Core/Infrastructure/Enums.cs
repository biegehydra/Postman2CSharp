namespace Postman2CSharp.Core.Infrastructure
{
    public enum DataType
    {
        Null,
        QueryOnly,
        SimpleFormData,
        ComplexFormData,
        Json,
        Xml,
        Html,
        Text,
        Binary,
        GraphQl
    }
    public enum XmlCommentTypes
    {
        ApiClient,
        QueryParameters,
        FormData,
        PathVariables,
        Request,
        Header
    }

    public enum GeneratedClassType
    {
        QueryParameters,
        FormData,
        Response,
        Request
    }

    public enum RootDefinition
    {
        PerAuthorityPerFolder,
        PerAuthority,
    }

    public enum ErrorHandlingSinks
    {
        LogException,
        ConsoleWriteLine,
        DebugWriteLine
    }

    public enum ErrorHandlingStrategy
    {
        None,
        ThrowException,
        ReturnDefault
    }

    public enum MultipleResponseHandling
    {
        OnlySuccessResponse,
        AllResponses
    }

    public enum CatchExceptionTypes
    {
        HttpRequestException,
        JsonException,
        Exception,
    }

    public enum LogLevel
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Critical
    }
    public enum FormDataType
    {
        Text,
        File
    }

    public enum RemoveDisabled
    {
        Headers,
        QueryParameters,
        FormData,
        FormUrlEncoded
    }
}
