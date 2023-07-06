// Copyright © 2010 Xamasoft

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xamasoft.JsonClassGenerator.CodeWriters;

using Xamasoft.JsonClassGenerator.Models;

namespace Xamasoft.JsonClassGenerator
{
    public class DuplicateRootException : Exception
    {
        public string OriginalRootName { get; set; }
        public bool OriginalIsRoot { get; set; }
    }
    public class JsonClassGenerator
    {
        public JsonClassGenerator(ICodeWriter codeWriter, bool removeDuplicateClasses, bool removeSemiDuplicates, bool removeDuplicateRoots)
        {
            CodeWriter = codeWriter;
            this.RemoveDuplicateClasses = removeDuplicateClasses;
            this.RemoveSemiDuplicates = removeSemiDuplicates;
            this.RemoveDuplicateRoots = removeDuplicateRoots;
        }

        public void SetRootName(string rootClassName)
        {
            _rootClassName = rootClassName;
        }
        #region Dependencies
        public ICodeWriter CodeWriter { get; set; }

        public string GetTypeDescription(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return null;
            }
            if (DescriptionDict.TryGetValue(type, out var description))
            {
                return description;
            }
            return null;
        }

        public void SetDescriptionDict(Dictionary<string, string> descriptionDict)
        {
            DescriptionDict = descriptionDict ?? new Dictionary<string, string>();
        }

        private Dictionary<string, string> DescriptionDict { get; set; } = new();
        private bool RemoveDuplicateClasses { get; set; }
        private bool RemoveSemiDuplicates { get; set; }
        private bool RemoveDuplicateRoots { get; set; }
        private string _rootClassName = "Root";
        public List<JsonType> AllTypes { get; private set; } = new();
        public IList<JsonType> Types { get; private set; }
        public HashSet<string> Names = new HashSet<string>();
        #endregion

        /// <summary>
        /// Main Method for parsing json input
        /// </summary>
        /// <param name="jsonInput"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public StringBuilder GenerateClasses(string jsonInput, out string errorMessage)
        {
            JObject[] examples = null;
            bool rootWasArray = false;
            try
            {
                using (StringReader sr = new StringReader(jsonInput))
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    JToken json = JToken.ReadFrom(reader);
                    if (json is JArray jArray && (jArray.Count == 0 || jArray.All(el => el is JObject)))
                    {
                        rootWasArray = true;
                        examples = jArray.Cast<JObject>().ToArray();
                    }
                    else if (json is JObject jObject)
                    {
                        examples = new[] { jObject };
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Exception: " + ex.Message;
                return new StringBuilder();
            }

            try
            {
                if (this.CodeWriter == null) this.CodeWriter = new CSharpCodeWriter();

                this.Types = new List<JsonType>();
                this.Names.Add(_rootClassName ?? "Root"); //TODO Make sure this doesn't break anything
                JsonType rootType = new JsonType(this, examples[0]);
                rootType.IsRoot = true;
                rootType.AssignName(_rootClassName ?? "Root");
                this.GenerateClass(examples, rootType);

                this.Types = this.HandleDuplicateClasses(this.Types);
                AllTypes.AddRange(Types);
                LastMinuteCleansing(this.Types, this.AllTypes);


                StringBuilder builder = new StringBuilder();
                CodeWriter.WriteClassesToFile(builder, this.Types, rootWasArray);

                errorMessage = String.Empty;
                return builder;
            }
            catch (DuplicateRootException)
            {
                throw;
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                return new StringBuilder();
            }
        }

        public static void LastMinuteCleansing(IList<JsonType> types, List<JsonType> allTypes)
        {
            // TODO: Explain
            foreach (JsonType type in types)
            {
                foreach (JsonFieldInfo field in type.Fields)
                {
                    if (!allTypes.Any(x => FieldEqual(x, field)))
                    {
                        if (CSharpCodeWriter._reservedKeywords.Contains(field.Type?.NewAssignedName))
                        {
                            continue;
                        }
                        if (CSharpCodeWriter._reservedKeywords.Contains(field.Type?.InternalType?.NewAssignedName))
                        {
                            continue;
                        }
                        if (field.Type?.NewAssignedName is {Length: > 2})
                        {
                            var test = field.Type?.NewAssignedName;
                            var last = field.Type.NewAssignedName.Last();
                            if (char.IsDigit(last))
                            {
                                field.Type.AssignNewAssignedName(field.Type.NewAssignedName[..^1]);
                            }
                        }
                        if (field.Type?.InternalType?.NewAssignedName is {Length: > 2})
                        {
                            var test = field.Type?.InternalType.NewAssignedName;
                            var last = field.Type.InternalType.NewAssignedName.Last();
                            if (char.IsDigit(last))
                            {
                                field.Type.AssignNewAssignedName(field.Type.InternalType.NewAssignedName[..^1]);
                            }
                        }
                    }
                }
            }
        }

        private static bool FieldEqual(JsonType type, JsonFieldInfo field)
        {
            if (field == null) return false;
            if (field.Type?.NewAssignedName != null && field.Type.NewAssignedName == type.NewAssignedName)
            {
                return true;
            }
            if (field.Type?.InternalType?.NewAssignedName != null && field.Type.InternalType.NewAssignedName == type.NewAssignedName)
            {
                return true;
            }
            return false;
        }

        private void GenerateClass(JObject[] examples, JsonType type)
        {
            Dictionary<string, JsonType> jsonFields = new Dictionary<string, JsonType>();
            Dictionary<string, List<object>> fieldExamples = new Dictionary<string,List<object>>();

            Boolean first = true;

            foreach (JObject obj in examples)
            {
                foreach (JProperty prop in obj.Properties())
                {
                    JsonType fieldType;
                    JsonType currentType = new JsonType(this, prop.Value);
                    String propName = prop.Name;

                    if (jsonFields.TryGetValue(propName, out fieldType))
                    {
                        JsonType commonType = fieldType.GetCommonType(currentType);

                        jsonFields[propName] = commonType;
                    }
                    else
                    {
                        JsonType commonType = currentType;


                        if (!first) {
                            commonType = commonType.GetCommonType(JsonType.GetNull(this));
                        }

                        jsonFields.Add(propName, commonType);
                        fieldExamples[propName] = new List<object>();
                    }

                    List<Object> fe = fieldExamples[propName];
                    JToken val = prop.Value;
                    if (val.Type == JTokenType.Null || val.Type == JTokenType.Undefined)
                    {
                        if (!fe.Contains(null))
                        {
                            fe.Insert(0, null);
                        }
                    }
                    else
                    {
                        Object v = val.Type == JTokenType.Array || val.Type == JTokenType.Object ? val : val.Value<object>();
                        if (!fe.Any(x => v.Equals(x)))
                        {
                            fe.Add(v);
                        }
                    }
                }
                first = false;
            }

            // Remove support for nested classes for now until code is cleaned
            // if (this.UseNestedClasses)
            // {
            //     foreach (KeyValuePair<String, JsonType> field in jsonFields)
            //     {
            //         this.Names.Add(field.Key.ToLower());
            //     }
            // }

            foreach (KeyValuePair<String, JsonType> field in jsonFields)
            {
                JsonType fieldType = field.Value;
                if (fieldType.Type == JsonTypeEnum.Object)
                {
                    List<JObject> subexamples = new List<JObject>(examples.Length);
                    foreach (JObject obj in examples)
                    {
                        JToken value;
                        if (obj.TryGetValue(field.Key, out value))
                        {
                            if (value.Type == JTokenType.Object)
                            {
                                subexamples.Add((JObject)value);
                            }
                        }
                    }

                    fieldType.AssignOriginalName(field.Key);
                    fieldType.AssignName(this.CreateUniqueClassName(field.Key));
                    fieldType.AssignNewAssignedName(field.Key.ToTitleCase());

                    this.GenerateClass(subexamples.ToArray(), fieldType);
                }

                if (fieldType.InternalType != null && fieldType.InternalType.Type == JsonTypeEnum.Object)
                {
                    List<JObject> subexamples = new List<JObject>(examples.Length);
                    foreach (JObject obj in examples)
                    {
                        JToken value;
                        if (obj.TryGetValue(field.Key, out value))
                        {
                            if (value is JArray jArray)
                            {
                                const int MAX_JSON_ARRAY_ITEMS = 50; // Take like 30 items from the array this will increase the chance of getting all the objects accuralty while not analyzing all the data

                                subexamples.AddRange(jArray.OfType<JObject>().Take(MAX_JSON_ARRAY_ITEMS));
                            }
                            else if (value is JObject jObject) //TODO J2C : ONLY LOOP OVER 50 OBJECT AND NOT THE WHOLE THING
                            {
                                foreach (KeyValuePair<String, JToken> jsonObjectProperty in jObject)
                                {
                                    // if (!(item.Value is JObject)) throw new NotSupportedException("Arrays of non-objects are not supported yet.");
                                    if (jsonObjectProperty.Value is JObject innerObject)
                                    {
                                        subexamples.Add(innerObject);
                                    }
                                }
                            }
                        }
                    }

                    field.Value.InternalType.AssignOriginalName(field.Key);
                    field.Value.InternalType.AssignName(this.CreateUniqueClassNameFromPlural(field.Key));
                    field.Value.InternalType.AssignNewAssignedName(field.Key.ToTitleCase());

                    this.GenerateClass(subexamples.ToArray(), field.Value.InternalType);
                }
            }

            type.Fields = jsonFields
                .Select(x => new JsonFieldInfo(
                    generator          : this,
                    jsonMemberName     : x.Key,
                    type               : x.Value,
                    examples           : fieldExamples[x.Key])
                )
                .ToList();

            if (!string.IsNullOrEmpty(type.AssignedName))
            {
                this.Types.Add(type);
            }
        }

        /// <summary>
        /// Checks if there are any duplicate classes in the input, and merges its corresponding properties (TEST CASE 7)
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        private IList<JsonType> HandleDuplicateClasses(IList<JsonType> types)
        {
            // TODO: This is currently O(n*n) because it iterates through List<T> on every loop iteration. This can be optimized.

            List<JsonType> typesWithNoDuplicates = new List<JsonType>();
            types = types.OrderByDescending(x => x.IsRoot).ThenBy(p => p.AssignedName).ToList();
            AllTypes = AllTypes.OrderBy(p => p.AssignedName).ToList();

            foreach (JsonType type in types)
            {
                bool isDuplicate = false;
                if (this.RemoveDuplicateClasses)
                {
#if DEBUG
                    var test = type.AssignedName;
                    var test2 = type.OriginalName;
                    var test3 = type.NewAssignedName;
                    var test4 = type.Fields;
                    var test5 = type.InternalType;
                    var test6 = type.Type;
#endif
                    var matchedTypes = AllTypes.Where(x => (x.OriginalName != null && x.OriginalName == type.OriginalName) || (x.OriginalName == null && x.AssignedName == type.AssignedName)).ToList();
                    foreach (JsonType matchedType in matchedTypes)
                    {
                        // If there are two types in different files/input strings with exactly the same class name and properties we don't 
                        // need to generate the same class twice so we mark it as a duplicate and skip
                        if (TypesMatch(type, matchedType, this.RemoveDuplicateRoots))
                        {
                            isDuplicate = true;
                            break;
                        }
                        else
                        {
                            // If the type wasn't a match that means we are keeping the duplicate.
                            // Because of this we check if the assigned names equal Class2 == Class2
                            // Then we set the internal type of it to the duplicate
                            // Before: AssignedName: Class2, NewAssignedName: Class => After: AssignedName: Class2, NewAssignedName: Class2
                            foreach (JsonType jsonType in types)
                            {
                                foreach (JsonFieldInfo jsonTypeField in jsonType.Fields)
                                {
                                    if (type.AssignedName != null && jsonTypeField.Type?.AssignedName == type.AssignedName)
                                    {
                                        jsonTypeField.Type!.AssignOriginalName(type.OriginalName);
                                        jsonTypeField.Type.AssignName(type.AssignedName);
                                        jsonTypeField.Type.AssignNewAssignedName(type.NewAssignedName);
                                        continue;
                                    }
                                    if (type.AssignedName != null && jsonTypeField.Type?.InternalType?.AssignedName == type.AssignedName)
                                    {
                                        jsonTypeField.Type!.InternalType!.AssignOriginalName(type.OriginalName);
                                        jsonTypeField.Type.InternalType.AssignName(type.AssignedName);
                                        jsonTypeField.Type!.InternalType.AssignNewAssignedName(type.NewAssignedName);
                                    }
                                }
                            }
                        }
                    }
                }
                if (isDuplicate) continue;

                // If this reference hasn't already been fixed above, we need to fix it here
                if (type.AssignedName != null && type.NewAssignedName != null && char.IsDigit(type.AssignedName.Last()) && !char.IsDigit(type.NewAssignedName.Last()))
                    type.AssignNewAssignedName(type.AssignedName);
                if (type.InternalType is {AssignedName: not null, NewAssignedName: not null} && char.IsDigit(type.InternalType.AssignedName.Last()) && !char.IsDigit(type.InternalType.NewAssignedName.Last()))
                    type.AssignNewAssignedName(type.AssignedName);

                if (!RemoveDuplicateClasses || !typesWithNoDuplicates.Exists(p => p.OriginalName == type.OriginalName))
                {
                    typesWithNoDuplicates.Add(type);
                }
                else
                {
                    JsonType duplicatedType = typesWithNoDuplicates.FirstOrDefault(p => p.OriginalName == type.OriginalName);
                    // Rename all references of this type to the original assigned name
                    foreach (JsonFieldInfo field in type.Fields)
                    {
                        if (!duplicatedType.Fields.ToList().Exists(x => x.JsonMemberName == field.JsonMemberName))
                        {
                            duplicatedType.Fields.Add(field);
                        }
                    }
                }
            }

            if (this.RemoveSemiDuplicates)
            {
                var semiDuplicates = new List<JsonType>();
                foreach (JsonType possibleDuplicate in typesWithNoDuplicates)
                {
                    foreach (JsonType nonDuplicate in AllTypes)
                    {
                        if (TypesMatch(possibleDuplicate, nonDuplicate, this.RemoveDuplicateRoots))
                        {
                            semiDuplicates.Add(( possibleDuplicate ));
                            foreach (JsonType type in typesWithNoDuplicates)
                            {
                                foreach (JsonFieldInfo jsonTypeField in type.Fields)
                                {
                                    // Here we are reassigning the references of the semi duplicates. If the original NewAssignedName was
                                    // Amount and the duplicate is ShippingAmount, we want to reassign all references of ShippingAmount to Amount
                                    // So that that there are less overall classes. Below here we remove the semi duplicates from
                                    // typesWithNoDuplicates. In the example, we would be removing ShippingAmount and keeping Amount.
                                    // It won't cause an issue because all references have been reassigned.
                                    if (possibleDuplicate.NewAssignedName != null && jsonTypeField.Type?.NewAssignedName == possibleDuplicate.NewAssignedName)
                                    {
                                        jsonTypeField.Type.AssignOriginalName(nonDuplicate.OriginalName);
                                        jsonTypeField.Type.AssignName(nonDuplicate.AssignedName);
                                        jsonTypeField.Type.AssignNewAssignedName(nonDuplicate.NewAssignedName ?? nonDuplicate.AssignedName);
                                        continue;
                                    }
                                    if (possibleDuplicate.NewAssignedName != null && jsonTypeField.Type?.InternalType?.NewAssignedName == possibleDuplicate.NewAssignedName)
                                    {
                                        jsonTypeField.Type.InternalType.AssignOriginalName(nonDuplicate.OriginalName);
                                        jsonTypeField.Type!.InternalType.AssignName(nonDuplicate.AssignedName);
                                        jsonTypeField.Type!.InternalType!.AssignNewAssignedName(nonDuplicate.NewAssignedName ?? nonDuplicate.AssignedName);
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var type in semiDuplicates)
                {
                    typesWithNoDuplicates.Remove(type);
                }
            }

            RemoveUnusedTypes(typesWithNoDuplicates);

            return typesWithNoDuplicates;
        }

        private static void RemoveUnusedTypes(List<JsonType> typesWithNoDuplicates)
        {
            var unused = new List<JsonType>();
            foreach (JsonType jsonType in typesWithNoDuplicates)
            {
                bool isUsed = false;
                foreach (JsonType typesWithNoDuplicate in typesWithNoDuplicates)
                {
                    if (typesWithNoDuplicate.Fields.Any(x =>
                            x.Type?.NewAssignedName == jsonType.NewAssignedName ||
                            x.Type?.InternalType?.NewAssignedName == jsonType.NewAssignedName))
                    {
                        isUsed = true;
                        break;
                    }
                }

                if (!isUsed)
                {
                    unused.Add(jsonType);
                }
            }

            foreach (var type in unused)
            {
                typesWithNoDuplicates.Remove(type);
            }
        }

        private static bool TypesMatch(JsonType dup, JsonType orig, bool removeDuplicateRoots)
        {
            if (dup.Fields == null && orig.Fields != null) return false;
            if (dup.Fields != null && orig.Fields == null) return false;
            if (dup.Fields == null && orig.Fields == null) return true;
            if (dup.Fields!.Count > orig.Fields!.Count) return false;
            if (dup.OriginalName != orig.OriginalName &&
                orig.Fields.Count - dup.Fields.Count > 4)
            {
                return false;
            }
            if (!removeDuplicateRoots && (dup.IsRoot || orig.IsRoot)) return false;
            foreach (JsonFieldInfo jsonFieldInfo in dup.Fields!)
            {
                if (orig.Fields!.Any(x => x.MemberName == jsonFieldInfo.MemberName && x.Type.Type == jsonFieldInfo.Type.Type))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            if (dup.IsRoot && removeDuplicateRoots)
            {
                throw new DuplicateRootException()
                {
                    OriginalRootName = orig.NewAssignedName ?? orig.AssignedName,
                    OriginalIsRoot = orig.IsRoot
                };
            }
            return true;
        }
        

        private string CreateUniqueClassName(string name)
        {
            name = name.ToTitleCase();
            String finalName = name;
            Int32 i = 2;
            while (Names.Any(x => x.Equals(finalName, StringComparison.OrdinalIgnoreCase)))
            {
                finalName = name + i;
                i++;
            }
            this.Names.Add(finalName);
            return finalName;
        }

        private string CreateUniqueClassNameFromPlural(string plural)
        {
            return this.CreateUniqueClassName(plural);
        }
    }
}
