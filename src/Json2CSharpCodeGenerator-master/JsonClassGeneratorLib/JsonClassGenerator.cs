// Copyright © 2010 Xamasoft

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
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
        public bool DuplicateIsArray { get; set; }
    }

    public class TextWasNotJsonException : Exception
    {
        public required string Text { get; set; }
    }

    public class DuplicateOptions
    {
        public bool RemoveSemiDuplicates { get; set; } = true;
        public int SameOriginalNameSensitivity { get; set; } = 3;
        public int DifferentOriginalNameSensitivity { get; set; } = 2;
        public bool RemoveDuplicateRoots { get; set; } = true;

        public DuplicateOptions Clone()
        {
            return (DuplicateOptions) MemberwiseClone();
        }
    }

    public class JsonClassGenerator
    {
        public void SetCurrentRootIsQueryParameters(bool currentRootIsQueryParameters)
        {
            _currentRootIsQueryParameters = currentRootIsQueryParameters;
        }
        private bool _currentRootIsQueryParameters; 

        public JsonClassGenerator(ICodeWriter codeWriter, DuplicateOptions duplicateOptions)
        {
            CodeWriter = codeWriter;
            this.DuplicateOptions = duplicateOptions;
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
       
        public DuplicateOptions DuplicateOptions { get; set; }
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
        public (StringBuilder Sb, bool RootWasArray) GenerateClasses(string jsonInput, bool generateAllClasses, bool oneOriginalName, out string errorMessage)
        {
            JObject[] examples = null;
            bool rootWasArray = false;
            bool isXml = false;
            try
            {
                examples = GetExamples(jsonInput);
            }
            catch (JsonReaderException)
            {
                XmlDocument doc = new ();
                doc.LoadXml(jsonInput);
                jsonInput = JsonConvert.SerializeXmlNode(doc);
                examples = GetExamples(jsonInput);
                isXml = true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception: " + ex.Message;
                throw;
            }

            if (examples == null)
            {
                throw new TextWasNotJsonException()
                {
                    Text = jsonInput
                };
            }

            JObject[] GetExamples(string str)
            {
                using (StringReader sr = new StringReader(jsonInput))
                using (JsonTextReader reader = new JsonTextReader(sr))
                {
                    JToken json = JToken.ReadFrom(reader);
                    if (json is JArray jArray && ( jArray.Count == 0 || jArray.All(el => el is JObject) ))
                    {
                        rootWasArray = true;
                        return jArray.Cast<JObject>().ToArray();
                    }
                    else if (json is JObject jObject)
                    {
                        return new[] { jObject };
                    }
                }
                return null;
            }

            try
            {
                if (this.CodeWriter == null) this.CodeWriter = new CSharpCodeWriter();
                this.Types = new List<JsonType>();
                this.Names.Add(_rootClassName ?? "Root");
                JsonType rootType = new JsonType(this, examples[0]);
                rootType.IsRoot = true;
                rootType.OriginalAssignedName = _rootClassName ?? "Root";
                rootType.NewAssignedName = _rootClassName ?? "Root";
                rootType.RootWasArray = rootWasArray;
                this.GenerateClass(examples, rootType);
                if (_currentRootIsQueryParameters)
                {
                    var root = Types.FirstOrDefault(x => x.IsRoot);
                    if (root != null)
                    {
                        root.IsQueryParameters = true;
                    }

                }
                this.Types = this.HandleDuplicateClasses(this.Types, oneOriginalName);
                AllTypes.AddRange(Types);

                StringBuilder builder = new ();
                if (generateAllClasses)
                {
                    CodeWriter.WriteClassesToFile(builder, this.AllTypes, rootWasArray);
                }
                else
                {
                    CodeWriter.WriteClassesToFile(builder, this.Types, rootWasArray, isXml);
                }

                errorMessage = String.Empty;
                return (builder, rootWasArray);
            }
            catch (DuplicateRootException)
            {
                throw;
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
                throw;
            }
        }

        /// <summary>
        /// Main Method for parsing json input
        /// </summary>
        /// <param name="jsonInput"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public (IList<JsonType> Types, bool RootWasArray) GenerateTypes(string jsonInput, bool generateAllClasses, bool oneOriginalName)
        {
            JObject[] examples = null;
            bool rootWasArray = false;
            using (StringReader sr = new StringReader(jsonInput))
            using (JsonTextReader reader = new JsonTextReader(sr))
            {
                JToken json = JToken.ReadFrom(reader);
                if (json is JArray jArray && ( jArray.Count == 0 || jArray.All(el => el is JObject) ))
                {
                    rootWasArray = true;
                    examples = jArray.Cast<JObject>().ToArray();
                }
                else if (json is JObject jObject)
                {
                    examples = new[] { jObject };
                }
            }

            if (this.CodeWriter == null) this.CodeWriter = new CSharpCodeWriter();
            this.Types = new List<JsonType>();
            this.Names.Add(_rootClassName ?? "Root");
            JsonType rootType = new JsonType(this, examples[0]);
            rootType.IsRoot = true;
            rootType.OriginalAssignedName = _rootClassName ?? "Root";
            rootType.NewAssignedName = _rootClassName ?? "Root";
            rootType.RootWasArray = rootWasArray;
            this.GenerateClass(examples, rootType);
            if (_currentRootIsQueryParameters)
            {
                var root = Types.FirstOrDefault(x => x.IsRoot);
                if (root != null)
                {
                    root.IsQueryParameters = true;
                }

            }
            this.Types = this.HandleDuplicateClasses(this.Types, oneOriginalName);
            AllTypes.AddRange(Types);

            return (Types, rootWasArray);
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

                    fieldType.OriginalName = field.Key;
                    var titleCase = AddTitleToNamesIfNotPresent(field.Key);
                    fieldType.OriginalAssignedName = titleCase;
                    fieldType.NewAssignedName = titleCase;

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

                    field.Value.InternalType.OriginalName = field.Key;
                    var titleCase = this.AddTitleToNamesIfNotPresent(field.Key);
                    field.Value.InternalType.OriginalAssignedName = titleCase;
                    field.Value.InternalType.NewAssignedName = titleCase;

                    this.GenerateClass(subexamples.ToArray(), field.Value.InternalType);
                }
            }

            type.Fields = jsonFields
                .Select(x => new JsonFieldInfo(x.Key, x.Value, fieldExamples[x.Key]))
                .ToList();

            if (!string.IsNullOrEmpty(type.NewAssignedName))
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
        private IList<JsonType> HandleDuplicateClasses(IList<JsonType> types, bool oneOriginalName)
        {
            // TODO: This is currently O(n*n) because it iterates through List<T> on every loop iteration. This can be optimized.

            var rdp = this.DuplicateOptions.RemoveDuplicateRoots;
            var sons = this.DuplicateOptions.SameOriginalNameSensitivity;
            var dons = this.DuplicateOptions.DifferentOriginalNameSensitivity;
            bool removeSemiDuplicates = this.DuplicateOptions.RemoveSemiDuplicates;

            List<JsonType> typesWithNoDuplicates = new List<JsonType>();
            if (oneOriginalName)
            {
                types = types.OrderByDescending(x => !x.IsRoot).ThenByDescending(x => x.Fields?.Count).ThenBy(p => p.NewAssignedName).ToList();
            }
            else
            {
                types = types.OrderByDescending(x => x.IsRoot).ThenByDescending(x => x.Fields?.Count).ThenBy(p => p.NewAssignedName).ToList();
            }
            AllTypes = AllTypes.OrderBy(x => x.NewAssignedName).ToList();

            foreach (JsonType potentialDup in types)
            {
#if DEBUG
                var test = potentialDup;
#endif
                var firstMatchingType = typesWithNoDuplicates.FirstOrDefault(x => x.OriginalName == potentialDup.OriginalName);

                if (oneOriginalName)
                {
                    firstMatchingType ??= AllTypes.FirstOrDefault(x => !x.IsRoot && x.OriginalName == potentialDup.OriginalName);
                }

                if (firstMatchingType == null && typesWithNoDuplicates.FirstOrDefault(x =>
                        !potentialDup.IsRoot && !ReferenceEquals(potentialDup, x) &&
                        TypesMatch(potentialDup, x, rdp, sons, dons, !removeSemiDuplicates)) is { } matchingTypeDifferentOriginalName)
                {
                    firstMatchingType = matchingTypeDifferentOriginalName;
                    ChangeNewAssignedNamesByNewAssignedNameInList(types, potentialDup.NewAssignedName, matchingTypeDifferentOriginalName.NewAssignedName);
                    potentialDup.NewAssignedName = matchingTypeDifferentOriginalName.NewAssignedName;

                }

                if (firstMatchingType is null)
                {
                    typesWithNoDuplicates.Add(potentialDup);
                }
                else
                {
                    foreach (JsonFieldInfo field in potentialDup.Fields)
                    {
                        if (firstMatchingType.Fields.All(x => x.JsonMemberName != field.JsonMemberName))
                        {
                            firstMatchingType.Fields.Add(field);
                        }
                    }
                }
            }

            var duplicates = new List<JsonType>();
            foreach (JsonType potentialDup in typesWithNoDuplicates)
            {
#if DEBUG
                var test = potentialDup;

#endif

                if (AllTypes.FirstOrDefault(allTypeType => TypesMatch(potentialDup, allTypeType, rdp, sons, dons, !removeSemiDuplicates)) is { } allTypesMatchedType)
                {
                    duplicates.Add(potentialDup);
                    if (removeSemiDuplicates && ( potentialDup.NewAssignedName?.StartsWith('_') ?? false ) && potentialDup.Fields.Count == allTypesMatchedType.Fields.Count)
                    {

                    }
                    ChangeNewAssignedNamesByNewAssignedNameInList(typesWithNoDuplicates, potentialDup.NewAssignedName, allTypesMatchedType.NewAssignedName);
                    potentialDup.NewAssignedName = allTypesMatchedType.NewAssignedName;

                }
                else
                {
                    var confirmedNotDuplicate = potentialDup;
                    confirmedNotDuplicate.IsVariant = true;
                    if (AllTypes.Any(x => !x.IsRoot && x.NewAssignedName == confirmedNotDuplicate.NewAssignedName))
                    {
                        do
                        {
                            var newnewAssignedName = IncrementString(confirmedNotDuplicate.NewAssignedName);
                            confirmedNotDuplicate.NewAssignedName = newnewAssignedName;
                        } while (AllTypes.Any(x => !x.IsRoot && x.NewAssignedName == confirmedNotDuplicate.NewAssignedName));
                        ChangeNewAssignedNamesByOriginalInList(typesWithNoDuplicates, confirmedNotDuplicate.OriginalName, confirmedNotDuplicate.NewAssignedName);
                    }
                }
            }

            // There are some weird issues that occur when a json property starts with a number.
            // This is a hack to fix that.
            while (true)
            {
                JsonType duplicate = null;
                foreach (JsonType notDuplicate in typesWithNoDuplicates)
                {
                    if (typesWithNoDuplicates.FirstOrDefault(x =>
                            !ReferenceEquals(x, notDuplicate) &&
                            x.NewAssignedName == notDuplicate.NewAssignedName) is { } dup)
                    {
#if DEBUG
                        Console.WriteLine($"Dup Orig: {dup.OriginalName}, Dup Assigned: {dup.OriginalAssignedName}, Dup New Assigned: {dup.NewAssignedName}" +
                                        $", Not Dup Orig: {notDuplicate.OriginalName}, Not Dup Assigned: {notDuplicate.OriginalAssignedName}, Not Dup New Assigned: {notDuplicate.NewAssignedName}");
#endif
                        duplicate = dup;
                        foreach (JsonFieldInfo field in dup.Fields)
                        {
                            if (notDuplicate.Fields.All(x => x.JsonMemberName != field.JsonMemberName))
                            {
                                notDuplicate.Fields.Add(field);
                            }
                        }
                        break;
                    }
                }
                typesWithNoDuplicates.Remove(duplicate);
                if (duplicate == null)
                {
                    break;
                }
            }

            foreach (JsonType duplicate in duplicates)
            {
                if (typesWithNoDuplicates.Contains(duplicate))
                {
                    typesWithNoDuplicates.Remove(duplicate);
                }
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
                    if (originalName != null &&
                        jsonTypeField.Type?.OriginalName == originalName)
                    {
                        jsonTypeField.Type.NewAssignedName = newAssignedName;
                        continue;
                    }

                    if (originalName != null && jsonTypeField.Type?.InternalType?.OriginalName ==
                        originalName)
                    {
                        jsonTypeField.Type!.InternalType!.NewAssignedName = newAssignedName;
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
                    if (oldNewAssignedName != null &&
                        jsonTypeField.Type?.NewAssignedName == oldNewAssignedName)
                    {
                        jsonTypeField.Type.NewAssignedName = newNewAssignedName;
                        continue;
                    }

                    if (oldNewAssignedName != null && jsonTypeField.Type?.InternalType?.NewAssignedName ==
                        oldNewAssignedName)
                    {
                        jsonTypeField.Type!.InternalType!.NewAssignedName = newNewAssignedName;
                    }
                }
            }
        }

        private static void RemoveUnusedTypes(List<JsonType> typesWithNoDuplicates)
        {
            var unused = new List<JsonType>();
            foreach (JsonType jsonType in typesWithNoDuplicates)
            {
#if DEBUG
                var test = jsonType;
#endif
                if (jsonType.IsRoot)
                {
                    continue;
                }
                bool isUsed = false;
                foreach (JsonType typesWithNoDuplicate in typesWithNoDuplicates)
                {
#if DEBUG
                    var test2 = typesWithNoDuplicate;
#endif
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

        private static bool TypesMatch(JsonType dup, JsonType orig, bool removeDuplicateRoots, int sameOriginalNameSensitivity, int differentOriginalNameSensitivity, bool strict = false)
        {
            if (dup.IsQueryParameters || orig.IsQueryParameters) return false;
            if (dup.Fields == null && orig.Fields != null) return false;
            if (dup.Fields != null && orig.Fields == null) return false;
            if (dup.Fields == null && orig.Fields == null) return true;
            if (dup.Fields!.Count > orig.Fields!.Count) return false;
            if (strict && dup.Fields!.Count != orig.Fields!.Count) return false;
            if (dup.OriginalName != orig.OriginalName &&
                (dup.Fields!.Count == 0 || orig.Fields!.Count == 0 || orig.Fields.Count - dup.Fields.Count > differentOriginalNameSensitivity))
            {
                return false;
            }
            if (dup.OriginalName == orig.OriginalName &&
                orig.Fields.Count - dup.Fields.Count > sameOriginalNameSensitivity)
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
                    OriginalRootName = orig.NewAssignedName,
                    OriginalIsRoot = orig.IsRoot,
                    DuplicateIsArray = dup.IsArray
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
