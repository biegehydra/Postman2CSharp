using System;
using Postman2CSharp.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamasoft.JsonClassGenerator.Models;

namespace Postman2CSharp.Core.Infrastructure
{
    public static class Common
    {
        public static void AddDefaultRequestHeader(this StringBuilder sb, string indent, string key, string value)
        {
            sb.AppendLineIndented(indent, $@"_httpClient.DefaultRequestHeaders.Add({key}, {value});");
        }
        public static void SetDefaultAuthorizationHeader(this StringBuilder sb, string indent, string key, string value)
        {
            sb.AppendLineIndented(indent, $@"_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue({key}, {value});");
        }

        public static void AssignVariableToConfig(this StringBuilder sb, string indent, string variable, string path)
        {
            sb.AppendLineIndented(indent, $@"{variable} = config[""Path:To{path}""];");
        }

        public static void AddPrivateReadonlyString(this StringBuilder sb, string indent, string variableName)
        {
            sb.AppendLineIndented(indent, $"private readonly string {variableName};");
        }

        public static void AppendLineIndented(this StringBuilder sb, int indent, string str)
        {
            sb.AppendLine(Consts.Indent(indent) + str);
        }

        public static void AppendLineIndented(this StringBuilder sb, string indent, string str)
        {
            sb.AppendLine(indent + str);
        }

        public static void AppendIndented(this StringBuilder sb, int indent, string str)
        {
            sb.Append(Consts.Indent(indent) + str);
        }

        public static void AppendIndented(this StringBuilder sb, string indent, string str)
        {
            sb.Append(indent + str);
        }

        public static void FunctionSignature(this StringBuilder sb, HttpCall call, string indent, List<HttpCallMethodParameter> methodParameters, ApiClientOptions options, bool isInterface = false)
        {
            if (isInterface)
            {
                sb.AppendIndented(indent, "Task");
            }
            else
            {
                sb.AppendIndented(indent, "public async Task");
            }
            if (!options.HandleMultipleResponses || call.AllResponses.GroupBy(x => SignatureClassName(x.ClassName, x.RootWasArray, options.OutputCollectionType)).Count() == 1)
            {
                if (call.SuccessResponse == null)
                {
#if DEBUG
                    throw new Exception("SuccessResponse null. Not supposed to happen.");
#endif
                }
                sb.Append($"<{SignatureClassName(call.SuccessResponse?.ClassName, call.SuccessResponse?.RootWasArray ?? false, options.OutputCollectionType)}>");
            }
            else
            {
                if (options.MultipleResponseHandling == MultipleResponseHandling.OneOf)
                {
                    sb.Append("<OneOf<");
                    var groups = call.AllResponses.GroupBy(x => SignatureClassName(x.ClassName, x.RootWasArray, options.OutputCollectionType));
                    sb.Append(string.Join(", ", groups.Select(x => x.Key)));
                    sb.Append(">>");
                }
                else if (options.MultipleResponseHandling == MultipleResponseHandling.Object)
                {
                    sb.Append("<object>");
                }
            }
            sb.Append($" {call.Name}(");
            sb.Append(string.Join(", ", methodParameters.Select(x => x.LocalParameterDeclaration)));
            sb.AppendLine(isInterface ? ");" : ")");
        }

        public static string SignatureClassName(string? className, bool rootWasArray,  OutputCollectionType outputCollectionType)
        {
            if (className == null)
            {
                return "Stream";
            }
            if (rootWasArray)
            {
                return outputCollectionType switch
                {
                    OutputCollectionType.Array or OutputCollectionType.ImmutableArray => $"{className}[]",
                    OutputCollectionType.MutableList or OutputCollectionType.IReadOnlyList => $"List<{className}>",
                    _ => throw new ArgumentOutOfRangeException(nameof(outputCollectionType), outputCollectionType, null)
                };
            }
            return className;
        }

        public static void ErrorHandlingStrategy(this StringBuilder sb, string throwDeclaration, ErrorHandlingStrategy errorHandlingStrategy,
            string indent)
        {
            switch (errorHandlingStrategy)
            {
                case Infrastructure.ErrorHandlingStrategy.None:
                    sb.AppendLineIndented(indent, throwDeclaration);
                    break;
                case Infrastructure.ErrorHandlingStrategy.ThrowException:
                    sb.AppendLineIndented(indent, throwDeclaration);
                    break;
                case Infrastructure.ErrorHandlingStrategy.ReturnDefault:
                    sb.AppendLineIndented(indent, "return default;");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(errorHandlingStrategy), errorHandlingStrategy, null);
            }
        }

        public static void ErrorHandlingSinks(this StringBuilder sb, List<ErrorHandlingSinks> errorHandlingSinks, LogLevel logLevel, string indent, string? errorMessage = null)
        {
            var nonLoggerMessage = errorMessage == null ? "ex" : $"$\"{errorMessage} - Ex: {{ex}}\"";
            var loggerMessage = errorMessage == null ? "ex" : $"ex, $\"{errorMessage}\"";


            foreach (var errorHandlingSink in errorHandlingSinks)
            {
                switch (errorHandlingSink)
                {
                    case Infrastructure.ErrorHandlingSinks.LogException:
                        var logFunction = logLevel switch
                        {
                            LogLevel.Trace => "LogTrace",
                            LogLevel.Debug => "LogDebug",
                            LogLevel.Information => "LogInformation",
                            LogLevel.Warning => "LogWarning",
                            LogLevel.Error => "LogError",
                            LogLevel.Critical => "LogCritical",
                            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
                        };
                        sb.AppendLineIndented(indent, $"_logger.{logFunction}({loggerMessage});");
                        break;
                    case Infrastructure.ErrorHandlingSinks.ConsoleWriteLine:
                        sb.AppendLineIndented(indent, $"Console.WriteLine({nonLoggerMessage});");
                        break;
                    case Infrastructure.ErrorHandlingSinks.DebugWriteLine:
                        sb.AppendLineIndented(indent, $"Debug.WriteLine({nonLoggerMessage});");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(errorHandlingSinks), errorHandlingSink, null);
                }
            }
        }
    }
}
