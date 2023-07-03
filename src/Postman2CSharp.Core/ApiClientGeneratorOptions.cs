using System.Diagnostics.CodeAnalysis;
using Xamasoft.JsonClassGenerator.CodeWriterConfiguration;

namespace Postman2CSharp.Core
{
    public class ApiClientGeneratorOptions
    {
        public required CSharpCodeWriterConfig CSharpCodeWriterConfig { get; init; }
        public required ApiClientOptions ApiClientOptions { get; init; }

        [SetsRequiredMembers]
        public ApiClientGeneratorOptions(CSharpCodeWriterConfig config, ApiClientOptions apiClientOptions)
        {
            CSharpCodeWriterConfig = config;
            ApiClientOptions = apiClientOptions;
            CSharpCodeWriterConfig.AttributeLibrary = ApiClientOptions.JsonLibrary;
        }
    }

}
