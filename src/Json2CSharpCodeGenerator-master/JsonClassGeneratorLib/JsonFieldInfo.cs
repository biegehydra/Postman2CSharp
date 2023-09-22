// Copyright © 2010 Xamasoft

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Xamasoft.JsonClassGenerator 
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class JsonFieldInfo
    {
        private string DebuggerDisplay => $"JMN: {JsonMemberName}, MN: {MemberName}, TOg: {Type?.OriginalName}, TNas: {Type?.NewAssignedName}";
        public JsonFieldInfo(string jsonMemberName, JsonType type, IReadOnlyList<object> examples)
        {
            this.JsonMemberName = jsonMemberName ?? throw new ArgumentNullException(nameof(jsonMemberName));
            this.MemberName = jsonMemberName;
            this.ContainsSpecialChars = IsContainsSpecialChars(this.MemberName);        
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
            this.Examples = examples ?? Array.Empty<object>();
        }
        public XmlType XmlType => JsonMemberName.Contains("@") 
            ? XmlType.Attribute 
            : JsonMemberName.Contains("#text") 
            ? XmlType.Text 
            : XmlType.Element;

        /// <summary>Pascal-cased.</summary>
        public string MemberName { get; }
        /// <summary>Normally camelCased.</summary>
        public string JsonMemberName { get; }
        public JsonType Type { get; }
        public IReadOnlyList<object> Examples { get; }
        public bool ContainsSpecialChars { get; set; }

        private static bool IsContainsSpecialChars(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                int number;
                if (i == 0 && int.TryParse(c.ToString(), out number))
                    return true;

                if (!char.IsLetterOrDigit(c) && c != '_')
                    return true;
            }

            return false;
        }
   
        public string GetExamplesText()
        {
            return string.Join(separator: ", ", values: this.Examples.Take(5).Select(x => JsonConvert.SerializeObject(x)));
        }

    }
    public enum XmlType
    {
        Element,
        Attribute,
        Text
    }
}