using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Postman2CSharp.Core.Models.PostmanCollection.Authorization;
using Postman2CSharp.Core.Models.PostmanCollection.Http.Request;
using Postman2CSharp.Core.Utilities;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http
{
    public class Url
    {
        /// <summary>
        /// Raw url -- http://localhost:62099/api/filestorage/uploadfiletos3
        /// </summary>
        [JsonRequired] public required string Raw { get; set; }
        /// <summary>
        /// Host -- localhost
        /// </summary>
        public List<string>? Host { get; set; }
        /// <summary>
        /// Http Protocol -- http
        /// </summary>
        public string? Protocol { get; set; }
        /// <summary>
        /// Path --- "api","filestorage","uploadfiletos3"
        /// </summary>
        public List<Path>? Path { get; set; }
        /// <summary>
        /// Port -- 62099
        /// </summary>
        public string? Port { get; set; }
        /// <summary>
        /// Query parameters, don't send if disabled
        /// </summary>
        public List<QueryParameter>? Query { get; set; }

        public List<KeyValueTypeDescription>? Variable { get; set; }
    }

    [JsonConverter(typeof(PathConverter))]
    public class Path
    {
        public string? Value { get; set; }
        public string LocalVariableName => Utils.NormalizeToCsharpPropertyName(Value, CsharpPropertyType.Local);
        public bool IsVariable()
        {
            return Value != null && Value.StartsWith(":");
        }

        [JsonConstructor]
        public Path(string? value)
        {
            Value = value;
        }

        public static implicit operator Path(string value)
        {
            return new Path(value);
        }

        public static implicit operator string(Path path)
        {
            return path.Value!;
        }

        // Add additional methods here.
    }
    public class PathConverter : JsonConverter<Path>
    {
        public override Path Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Path(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Path value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}