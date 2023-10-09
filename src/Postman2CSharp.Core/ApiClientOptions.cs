using System.Collections.Generic;
using System.Linq;
using Postman2CSharp.Core.Infrastructure;
using Xamasoft.JsonClassGenerator;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core
{
    public class ApiClientOptions
    {
        public DuplicateOptions DuplicateOptions { get; set; } = new();
        public bool MakePathCollectionVariablesFunctionParameters { get; set; }
        public bool UseCancellationTokens { get; set; }
        public bool HandleMultipleResponses { get; set; } = true;
        public MultipleResponseHandling MultipleResponseHandling { get; set; } = MultipleResponseHandling.OneOf;
        public OutputCollectionType OutputCollectionType { get; set; } = OutputCollectionType.MutableList;
        public JsonLibrary JsonLibrary { get; set; } = JsonLibrary.SystemTextJson;

        public bool ExecuteWithRetry = false;

        public List<ErrorHandlingSinks> ErrorHandlingSinks { get; set; } = new()
        {
            Infrastructure.ErrorHandlingSinks.LogException
        };

        public List<CatchExceptionTypes> CatchExceptionTypes { get; set; } = new()
        {
            Infrastructure.CatchExceptionTypes.HttpRequestException
        };
        public JsonPropertyAttributeUsage AttributeUsage { get; set; } = JsonPropertyAttributeUsage.Always;
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
            Infrastructure.XmlCommentTypes.Header
        };
        public List<RemoveDisabled> RemoveDisabled { get; set; } = new();

        public ApiClientOptions Clone()
        {
            var memberWiseClone =  (ApiClientOptions) MemberwiseClone();
            memberWiseClone.DuplicateOptions = DuplicateOptions.Clone();
            return memberWiseClone;
        }

        public static bool ConfigsEqual(ApiClientOptions? options1, ApiClientOptions? options2)
        {
            if (options1 == null ||  options2 == null) return false;
            var allRemovedDisabledMatch = options1.RemoveDisabled.Count == options2.RemoveDisabled.Count && options1.RemoveDisabled.All(options2.RemoveDisabled.Contains);
            var allCommentTypesMatch = options1.XmlCommentTypes.Count == options2.XmlCommentTypes.Count && options1.XmlCommentTypes.All(options2.XmlCommentTypes.Contains);
            var allSinksMatch = options1.ErrorHandlingSinks.Count == options2.ErrorHandlingSinks.Count && options1.ErrorHandlingSinks.All(options2.ErrorHandlingSinks.Contains);
            var allCatchTypesMatch = options1.CatchExceptionTypes.Count == options2.CatchExceptionTypes.Count && options1.CatchExceptionTypes.All(options2.CatchExceptionTypes.Contains);
            return options1.MakePathCollectionVariablesFunctionParameters == options2.MakePathCollectionVariablesFunctionParameters
                   && options1.ErrorHandlingStrategy == options2.ErrorHandlingStrategy
                   && options1.LogLevel == options2.LogLevel
                   && options1.RootDefinition == options2.RootDefinition
                   && options1.UseCancellationTokens == options2.UseCancellationTokens
                   && options1.JsonLibrary == options2.JsonLibrary
                   && options1.AttributeUsage == options2.AttributeUsage
                   && options1.ExecuteWithRetry == options2.ExecuteWithRetry
                   && options1.MultipleResponseHandling == options2.MultipleResponseHandling
                   && options1.HandleMultipleResponses == options2.HandleMultipleResponses
                   && options1.DuplicateOptions.RemoveDuplicateRoots == options2.DuplicateOptions.RemoveDuplicateRoots
                   && options1.DuplicateOptions.RemoveSemiDuplicates == options2.DuplicateOptions.RemoveSemiDuplicates
                   && options1.DuplicateOptions.SameOriginalNameSensitivity == options2.DuplicateOptions.SameOriginalNameSensitivity
                   && options1.DuplicateOptions.DifferentOriginalNameSensitivity == options2.DuplicateOptions.DifferentOriginalNameSensitivity
                   && allSinksMatch && allCatchTypesMatch && allCommentTypesMatch && allRemovedDisabledMatch;
        }
    }
}
