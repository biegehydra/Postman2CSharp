using System.Collections.Generic;
using Postman2CSharp.Core.Infrastructure;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core
{
    public class ApiClientOptions
    {
        public bool RemoveDuplicateClasses { get; set; } = true;
        public bool EnsureResponseIsSuccessStatusCode { get; set; }
        public bool MakePathCollectionVariablesFunctionParameters { get; set; }
        public bool UseCancellationTokens { get; set; }

        public JsonLibrary JsonLibrary { get; set; } = JsonLibrary.SystemTextJson;

        public List<ErrorHandlingSinks> ErrorHandlingSinks { get; set; } = new()
        {
            Infrastructure.ErrorHandlingSinks.LogException
        };

        public List<CatchExceptionTypes> CatchExceptionTypes { get; set; } = new()
        {
            Infrastructure.CatchExceptionTypes.HttpRequestException
        };
        public RootDefinition RootDefinition { get; set; } = RootDefinition.PerAuthorityPerFolder;
        public ErrorHandlingStrategy ErrorHandlingStrategy { get; set; } = ErrorHandlingStrategy.None;
        public LogLevel LogLevel { get; set; } = LogLevel.Error;
        public List<XmlCommentTypes> XmlCommentTypes { get; set; } = new()
        {
            Infrastructure.XmlCommentTypes.ApiClient,
            Infrastructure.XmlCommentTypes.QueryParameters,
            Infrastructure.XmlCommentTypes.FormData,
            Infrastructure.XmlCommentTypes.PathVariables,
            Infrastructure.XmlCommentTypes.Request,
        };
        public List<RemoveDisabled> RemoveDisabled { get; set; } = new();

        public ApiClientOptions Clone()
        {
            return (ApiClientOptions) MemberwiseClone();
        }
    }
}
