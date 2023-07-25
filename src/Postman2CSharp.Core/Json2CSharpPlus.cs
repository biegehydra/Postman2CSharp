using Postman2CSharp.Core.Utilities;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xamasoft.JsonClassGenerator;
using Xamasoft.JsonClassGenerator.CodeWriterConfiguration;
using Xamasoft.JsonClassGenerator.CodeWriters;

namespace Postman2CSharp.Core
{
    public class Json2CSharpPlus
    {
        private JsonClassGenerator _classGenerator;
        private CSharpCodeWriter _codeWriter;
        public Json2CSharpPlus(CSharpCodeWriterConfig config, DuplicateOptions duplicateOptions)
        {
            config.SetNoNamespace();
            _codeWriter = new CSharpCodeWriter(config, false);
            _classGenerator = new JsonClassGenerator(_codeWriter, duplicateOptions);
        }
        private int TotalGeneratedClasses { get; set; }

        private string _rootClassName = "Root";
        private List<string> UsedNames { get; set; } = new();

        private Dictionary<string, (IList<JsonType> Types, bool RootWasArray)> TypeDictionary { get; set; } = new();

        public Dictionary<string,string> GenerateMoreClasses(string json, bool oneTypePerJsonMemberName)
        {
            _rootClassName = Utils.GenerateUniqueName(_rootClassName, UsedNames);
            _classGenerator.SetRootName(_rootClassName);
            try
            {
                var types = _classGenerator.GenerateTypes(json, true, oneTypePerJsonMemberName);
                TypeDictionary.Add(_rootClassName, types);

                return GenerateSourceCodeDict();
            }
            catch (JsonException)
            {
                UsedNames.Remove(_rootClassName);
                throw;
            }
            catch (DuplicateRootException)
            {
                UsedNames.Remove(_rootClassName);
                return GenerateSourceCodeDict();
            }

            Dictionary<string, string> GenerateSourceCodeDict()
            {
                TotalGeneratedClasses = 0;
                var sourceCodeDict = new Dictionary<string, string>();
                foreach (var (rootName, (classTypes, rootWasArray)) in TypeDictionary)
                {
                    var sb = new StringBuilder();
                    _codeWriter.WriteClassesToFile(sb, classTypes, rootWasArray);
                    sourceCodeDict.Add(rootName, CodeAnalysisUtils.ReorderClassesNoNamespace(sb.ToString(), rootName, out var classCount));
                    TotalGeneratedClasses += classCount;
                }
                return sourceCodeDict;
            }
        }
    }
}
