using System.Collections.Generic;

namespace Postman2CSharp.Core
{
    public class ApiClientOptions
    {
        public bool RemoveDuplicateClasses { get; set; } = true;
        public bool RemoveDisabledHeaders { get; set; }
        public bool RemoveDisabledQueryParams { get; set; }
        public bool EnsureResponseIsSuccessStatusCode { get; set; }
        public bool MakePathCollectionVariablesFunctionParameters { get; set; }

        public List<ErrorHandlingSinks> ErrorHandlingSinks { get; set; } = new()
        {
            Postman2CSharp.Core.ErrorHandlingSinks.LogException
        };

        public List<CatchExceptionTypes> CatchExceptionTypes { get; set; } = new()
        {
            Postman2CSharp.Core.CatchExceptionTypes.HttpRequestException
        };

        public RootDefinition RootDefinition { get; set; } = RootDefinition.PerAuthorityPerFolder;
        public ErrorHandlingStrategy ErrorHandlingStrategy { get; set; } = ErrorHandlingStrategy.None;
        public LogLevel LogLevel { get; set; } = LogLevel.Error;
        public List<XmlCommentTypes> XmlCommentTypes { get; set; } = new()
        {
            Postman2CSharp.Core.XmlCommentTypes.ApiClient,
            Postman2CSharp.Core.XmlCommentTypes.QueryParameters,
            Postman2CSharp.Core.XmlCommentTypes.FormData,
            Postman2CSharp.Core.XmlCommentTypes.PathVariables,
            Postman2CSharp.Core.XmlCommentTypes.Request,
        };

        public ApiClientOptions Clone()
        {
            return (ApiClientOptions) MemberwiseClone();
        }
    }

    public enum XmlCommentTypes
    {
        ApiClient,
        QueryParameters,
        FormData,
        PathVariables,
        Request,
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

    public enum CatchExceptionTypes
    {
        HttpRequestException,
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
}
