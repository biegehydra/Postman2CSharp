using Postman2CSharp.Core.Utilities;
using System.Collections.Generic;
using Xamasoft.JsonClassGenerator;
using Xamasoft.JsonClassGenerator.CodeWriterConfiguration;
using Xamasoft.JsonClassGenerator.CodeWriters;

namespace Postman2CSharp.Core
{
    public class Json2CSharpPlusSimple
    {
        private JsonClassGenerator _classGenerator;
        public Json2CSharpPlusSimple(CSharpCodeWriterConfig config, DuplicateOptions duplicateOptions)
        {
            var codeWriter = new CSharpCodeWriter(config, false);
            _classGenerator = new JsonClassGenerator(codeWriter, duplicateOptions);
        }
        private int TotalGeneratedClasses { get; set; }

        private string _rootClassName = "Root";
        private List<string> GeneratedSourceCodes { get; set; } = new();

        public string GenerateMoreClasses(string json)
        {
            _rootClassName = Utils.IncrementString(_rootClassName);
            var sb = _classGenerator.GenerateClasses(json, out var errorMessage);
            var consolidated = CodeAnalysisUtils.ConsolidateNamespaces(sb.ToString(), _rootClassName);
            var reordered = CodeAnalysisUtils.ReorderClasses(consolidated, _rootClassName, out var classCount);
            GeneratedSourceCodes.Add(reordered);
            TotalGeneratedClasses += classCount;
            var consolidatedAll = CodeAnalysisUtils.ConsolidateNamespaces(GeneratedSourceCodes, _rootClassName);
            var reorderedAll = CodeAnalysisUtils.ReorderClasses(consolidatedAll, _rootClassName, out _);
            return reorderedAll;
        }
    }
}
