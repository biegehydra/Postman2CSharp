// Copyright © 2010 Xamasoft

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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

        private static int TotalProcessed { get; set; }
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
                TotalProcessed++;
                this.Types = new List<JsonType>();
                this.Names.Add(_rootClassName ?? "Root"); //TODO Make sure this doesn't break anything
                JsonType rootType = new JsonType(this, examples[0]);
                rootType.IsRoot = true;
                rootType.AssignName(_rootClassName ?? "Root");
                this.GenerateClass(examples, rootType);

                this.Types = this.HandleDuplicateClassesJustTypes(this.Types);
                AllTypes.AddRange(Types);


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

        //public static void LastMinuteCleansing(IList<JsonType> types, List<JsonType> allTypes)
        //{
        //    // TODO: Explain
        //    foreach (JsonType type in types)
        //    {
        //        foreach (JsonFieldInfo field in type.Fields)
        //        {
        //            if (!allTypes.Any(x => FieldEqual(x, field)))
        //            {
        //                if (CSharpCodeWriter._reservedKeywords.Contains(field.Type?.NewAssignedName))
        //                {
        //                    continue;
        //                }
        //                if (CSharpCodeWriter._reservedKeywords.Contains(field.Type?.InternalType?.NewAssignedName))
        //                {
        //                    continue;
        //                }
        //                if (field.Type?.NewAssignedName is {Length: > 2})
        //                {
        //                    var test = field.Type?.NewAssignedName;
        //                    var last = field.Type.NewAssignedName.Last();
        //                    if (char.IsDigit(last))
        //                    {
        //                        field.Type.AssignNewAssignedName(field.Type.NewAssignedName[..^1]);
        //                    }
        //                }
        //                if (field.Type?.InternalType?.NewAssignedName is {Length: > 2})
        //                {
        //                    var test = field.Type?.InternalType.NewAssignedName;
        //                    var last = field.Type.InternalType.NewAssignedName.Last();
        //                    if (char.IsDigit(last))
        //                    {
        //                        field.Type.AssignNewAssignedName(field.Type.InternalType.NewAssignedName[..^1]);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private static bool FieldEqual(JsonType type, JsonFieldInfo field)
        //{
        //    if (field == null) return false;
        //    if (field.Type?.NewAssignedName != null && field.Type.NewAssignedName == type.NewAssignedName)
        //    {
        //        return true;
        //    }
        //    if (field.Type?.InternalType?.NewAssignedName != null && field.Type.InternalType.NewAssignedName == type.NewAssignedName)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

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
                    var titleCase = AddTitleToNamesIfNotPresent(field.Key);
                    fieldType.AssignName(titleCase);
                    fieldType.AssignNewAssignedName(titleCase);

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
                    var titleCase = this.AddTitleToNamesIfNotPresent(field.Key);
                    field.Value.InternalType.AssignName(titleCase);
                    field.Value.InternalType.AssignNewAssignedName(titleCase);

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

        private string AddTitleToNamesIfNotPresent( string original)
        {
            var title = original.ToTitleCase();
            // Hashset, don't need to check for duplicates
            Names.Add(title);
            return title;
        }

        /// <summary>
        /// Checks if there are any duplicate classes in the input, and merges its corresponding properties (TEST CASE 7)
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        private IList<JsonType> HandleDuplicateClassesJustTypes(IList<JsonType> types)
        {
            // TODO: This is currently O(n*n) because it iterates through List<T> on every loop iteration. This can be optimized.

            List<JsonType> typesWithNoDuplicates = new List<JsonType>();
            types = types.OrderByDescending(x => x.IsRoot).ThenBy(p => p.AssignedName).ToList();

            foreach (JsonType potentialDuplicate in types)
            {
#if DEBUG
                var test = potentialDuplicate.AssignedName;
                var test2 = potentialDuplicate.OriginalName;
                var test3 = potentialDuplicate.NewAssignedName;
                var test4 = potentialDuplicate.Fields;
                var test5 = potentialDuplicate.InternalType;
                var test6 = potentialDuplicate.Type;
                var test7 = potentialDuplicate.IsRoot;
                var test8 = potentialDuplicate.IsVariant;
#endif
                var firstMatchingType =
                    typesWithNoDuplicates.FirstOrDefault(x => x.OriginalName == potentialDuplicate.OriginalName);
                if (firstMatchingType is null)
                {
                    typesWithNoDuplicates.Add(potentialDuplicate);
                }
                else
                {
                    foreach (JsonFieldInfo field in potentialDuplicate.Fields)
                    {
                        if (firstMatchingType.Fields.All(x => x.JsonMemberName != field.JsonMemberName))
                        {
                            firstMatchingType.Fields.Add(field);
                        }
                    }
                }
            }

            var duplicates = new List<JsonType>();
            foreach (JsonType potentialType in typesWithNoDuplicates)
            {
#if DEBUG
                var test = potentialType.AssignedName;
                var test2 = potentialType.OriginalName;
                var test3 = potentialType.NewAssignedName;
                var test4 = potentialType.Fields;
                var test5 = potentialType.InternalType;
                var test6 = potentialType.Type;
                var test7 = potentialType.IsRoot;
                var test8 = potentialType.IsVariant;
#endif
                if (AllTypes.FirstOrDefault(allTypeType => ( potentialType.IsRoot && TypesMatch(potentialType, allTypeType, this.RemoveDuplicateRoots) ) ||
                                                           ( !potentialType.IsRoot && potentialType.OriginalName == allTypeType.OriginalName && TypesMatch(potentialType, allTypeType, this.RemoveDuplicateRoots) )) is
                    { } allTypesMatchedType)
                {
                    if (potentialType.IsRoot)
                    {
                        Console.WriteLine("t");
                    }
                    duplicates.Add(potentialType);
                    ChangeNewAssignedNamesByNewAssignedNameInList(types, potentialType.NewAssignedName ?? potentialType.AssignedName, allTypesMatchedType.NewAssignedName ?? allTypesMatchedType.AssignedName);
                }
                else
                {
                    var confirmedNotDuplicate = potentialType;
                    confirmedNotDuplicate.IsVariant = true;
                    if (AllTypes.Any(x => !x.IsRoot && x.NewAssignedName == confirmedNotDuplicate.NewAssignedName))
                    {
                        do
                        {
                            var newnewAssignedName = IncrementString(confirmedNotDuplicate.NewAssignedName);
                            confirmedNotDuplicate.AssignNewAssignedName(newnewAssignedName);
                        } while (AllTypes.Any(x => x.NewAssignedName == confirmedNotDuplicate.NewAssignedName));
                        ChangeNewAssignedNamesByOriginalInList(typesWithNoDuplicates, confirmedNotDuplicate.OriginalName, confirmedNotDuplicate.NewAssignedName);
                    }
                }
            }

            foreach (JsonType duplicate in duplicates)
            {
                typesWithNoDuplicates.Remove(duplicate);
            }

            RemoveUnusedTypes(typesWithNoDuplicates);

            return typesWithNoDuplicates;
        }

        private static void ChangeNewAssignedNamesByOriginalInList(IList<JsonType> listToReplaceAllReferencesIn, string originalName,
            string newAssignedName)
        {
            foreach (JsonType type in listToReplaceAllReferencesIn)
            {
                foreach (JsonFieldInfo jsonTypeField in type.Fields)
                {
                    // Here we are reassigning the references of the semi duplicates. If the original NewAssignedName was
                    // Amount and the duplicate is ShippingAmount, we want to reassign all references of ShippingAmount to Amount
                    // So that that there are less overall classes. Below here we remove the semi duplicates from
                    // typesWithNoDuplicates. In the example, we would be removing ShippingAmount and keeping Amount.
                    // It won't cause an issue because all references have been reassigned.
                    if (originalName != null &&
                        jsonTypeField.Type?.OriginalName == originalName)
                    {
                        jsonTypeField.Type.AssignNewAssignedName(newAssignedName);
                        continue;
                    }

                    if (originalName != null && jsonTypeField.Type?.InternalType?.OriginalName ==
                        originalName)
                    {
                        jsonTypeField.Type!.InternalType!.AssignNewAssignedName(newAssignedName);
                    }
                }
            }
        }

        private static void ChangeNewAssignedNamesByNewAssignedNameInList(IList<JsonType> listToReplaceAllReferencesIn, string oldNewAssignedName,
            string newNewAssignedName)
        {
            foreach (JsonType type in listToReplaceAllReferencesIn)
            {
                foreach (JsonFieldInfo jsonTypeField in type.Fields)
                {
                    if (type.IsRoot)
                    {
                        if (oldNewAssignedName != null &&
                            jsonTypeField.Type?.AssignedName == oldNewAssignedName)
                        {
                            jsonTypeField.Type.AssignNewAssignedName(newNewAssignedName);
                            continue;
                        }
                        if (oldNewAssignedName != null &&
                            jsonTypeField.Type?.InternalType?.AssignedName == oldNewAssignedName)
                        {
                            jsonTypeField.Type.InternalType.AssignNewAssignedName(newNewAssignedName);
                            continue;
                        }
                    }
                    else
                    {
                        if (oldNewAssignedName != null &&
                            jsonTypeField.Type?.NewAssignedName == oldNewAssignedName)
                        {
                            jsonTypeField.Type.AssignNewAssignedName(newNewAssignedName);
                            continue;
                        }

                        if (oldNewAssignedName != null && jsonTypeField.Type?.InternalType?.NewAssignedName ==
                            oldNewAssignedName)
                        {
                            jsonTypeField.Type!.InternalType!.AssignNewAssignedName(newNewAssignedName);
                        }
                    }
                }
            }
        }

        private static void RemoveUnusedTypes(List<JsonType> typesWithNoDuplicates)
        {
            var unused = new List<JsonType>();
            foreach (JsonType jsonType in typesWithNoDuplicates)
            {
                bool isUsed = false;
                foreach (JsonType typesWithNoDuplicate in typesWithNoDuplicates)
                {
                    if (typesWithNoDuplicate.IsRoot)
                    {
                        isUsed = true;
                        break;
                    }
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
            if (dup.IsRoot)
            {
                return false;
            }
            return true;
        }

        public static string IncrementString(string input)
        {
            // regular expression pattern for optional digits at the end of the string
            string pattern = @"^(.*?)(\d{1,2})?$";
            Regex regex = new (pattern);
            Match match = regex.Match(input);

            if (match.Success)
            {
                string prefix = match.Groups[1].Value; // the part of the string before the optional digits
                string numberStr = match.Groups[2].Value; // the optional digits at the end of the string

                int number;
                if (string.IsNullOrEmpty(numberStr))
                {
                    // if there was no number at the end of the string, we add 2 to it
                    number = 2;
                }
                else
                {
                    // if there was a number at the end of the string, we increment it by 1
                    number = int.Parse(numberStr) + 1;
                }

                // return the new string
                return prefix + number.ToString();
            }
            else
            {
                // if the string did not match the pattern, just return it unchanged
                return input;
            }
        }
    }
}
