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
            config.SetNoNamespace();
            var codeWriter = new CSharpCodeWriter(config, false);
            _classGenerator = new JsonClassGenerator(codeWriter, duplicateOptions);
        }
        private int TotalGeneratedClasses { get; set; }

        private string _rootClassName = "Root";
        private List<string> GeneratedSourceCodes { get; set; } = new();

        public string GenerateMoreClasses(string json)
        {
            _rootClassName = Utils.IncrementString(_rootClassName);
            var (sb, _) = _classGenerator.GenerateClasses(json, out var errorMessage);
            return sb.ToString();
        }
    }
}
