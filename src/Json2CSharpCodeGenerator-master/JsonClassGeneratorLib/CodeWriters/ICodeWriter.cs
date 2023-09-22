using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Xamasoft.JsonClassGenerator.CodeWriters
{
    public interface ICodeWriter
    {
        string GetTypeName(JsonType type);
        void WriteClassesToFile(StringBuilder sw, IEnumerable<JsonType> types, bool rootIsArray = false, bool isXml = false);
    }
}
