﻿// Copyright © 2010 Xamasoft

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                rootType.OriginalAssignedName = _rootClassName ?? "Root";
                rootType.NewAssignedName = _rootClassName ?? "Root";
                this.GenerateClass(examples, rootType);
                if (_currentRootIsQueryParameters)
                {
                    var root = Types.FirstOrDefault(x => x.IsRoot);
                    if (root != null)
                    {
                        root.IsQueryParameters = true;
                    }

                }
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
                .Select(x => new JsonFieldInfo(
                    generator          : this,
                    jsonMemberName     : x.Key,
                    type               : x.Value,
                    examples           : fieldExamples[x.Key])
                )
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
        private IList<JsonType> HandleDuplicateClassesJustTypes(IList<JsonType> types)
        {
            // TODO: This is currently O(n*n) because it iterates through List<T> on every loop iteration. This can be optimized.

            List<JsonType> typesWithNoDuplicates = new List<JsonType>();
            types = types.OrderByDescending(x => x.IsRoot).ThenBy(p => p.NewAssignedName).ToList();
            AllTypes = AllTypes.OrderBy(x => x.NewAssignedName).ToList();

            foreach (JsonType potentialDuplicate in types)
            {
#if DEBUG
                var test = potentialDuplicate.OriginalAssignedName;
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
                var test = potentialType.OriginalAssignedName;
                var test2 = potentialType.OriginalName;
                var test3 = potentialType.NewAssignedName;
                var test4 = potentialType.Fields;
                var test5 = potentialType.InternalType;
                var test6 = potentialType.Type;
                var test7 = potentialType.IsRoot;
                var test8 = potentialType.IsVariant;
#endif
                if (this.DuplicateOptions.RemoveSemiDuplicates && AllTypes.FirstOrDefault(allTypeType => ( potentialType.IsRoot && TypesMatch(potentialType, allTypeType, this.DuplicateOptions.RemoveDuplicateRoots, DuplicateOptions.SameOriginalNameSensitivity, DuplicateOptions.DifferentOriginalNameSensitivity) ) ||
                                                           ( !potentialType.IsRoot && potentialType.OriginalName == allTypeType.OriginalName && TypesMatch(potentialType, allTypeType, this.DuplicateOptions.RemoveDuplicateRoots, DuplicateOptions.SameOriginalNameSensitivity, DuplicateOptions.DifferentOriginalNameSensitivity) )) is
                    { } allTypesMatchedType)
                {
                    duplicates.Add(potentialType);
                    if ((potentialType.NewAssignedName?.StartsWith('_') ?? false) && potentialType.Fields.Count == allTypesMatchedType.Fields.Count)
                    {
                        
                    }
                    else
                    {
                        ChangeNewAssignedNamesByNewAssignedNameInList(types, potentialType.NewAssignedName, allTypesMatchedType.NewAssignedName);
                    }
                }
                else if (!this.DuplicateOptions.RemoveSemiDuplicates && AllTypes.FirstOrDefault(allTypeType =>
                                 ( potentialType.IsRoot &&
                                   TypesMatch(potentialType, allTypeType, this.DuplicateOptions.RemoveDuplicateRoots, DuplicateOptions.SameOriginalNameSensitivity, DuplicateOptions.DifferentOriginalNameSensitivity, true) ) ||
                                 ( !potentialType.IsRoot && potentialType.OriginalName == allTypeType.OriginalName &&
                                   TypesMatch(potentialType, allTypeType, this.DuplicateOptions.RemoveDuplicateRoots, DuplicateOptions.SameOriginalNameSensitivity, DuplicateOptions.DifferentOriginalNameSensitivity, true) )) is
                             { } allTypesMatchedTypeStrict)
                {
                    duplicates.Add(potentialType);
                }
                else
                {
                    var confirmedNotDuplicate = potentialType;
                    confirmedNotDuplicate.IsVariant = true;
                    if (AllTypes.Any(x => !x.IsRoot && x.NewAssignedName == confirmedNotDuplicate.NewAssignedName) || types.Any(x => !x.IsRoot && x != confirmedNotDuplicate && x.NewAssignedName == confirmedNotDuplicate.NewAssignedName))
                    {
                        do
                        {
                            var newnewAssignedName = IncrementString(confirmedNotDuplicate.NewAssignedName);
                            confirmedNotDuplicate.NewAssignedName = newnewAssignedName;
                        } while (AllTypes.Any(x => !x.IsRoot && x.NewAssignedName == confirmedNotDuplicate.NewAssignedName) || types.Any(x => !x.IsRoot && x != confirmedNotDuplicate &&  x.NewAssignedName == confirmedNotDuplicate.NewAssignedName));
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
                var test = jsonType.OriginalAssignedName;
                var test2 = jsonType.OriginalName;
                var test3 = jsonType.NewAssignedName;
                var test4 = jsonType.Fields;
                var test5 = jsonType.InternalType;
                var test6 = jsonType.Type;
                var test7 = jsonType.IsRoot;
                var test8 = jsonType.IsVariant;
#endif
                if (jsonType.IsRoot)
                {
                    continue;
                }
                bool isUsed = false;
                foreach (JsonType typesWithNoDuplicate in typesWithNoDuplicates)
                {
#if DEBUG
                    var test1 = jsonType.OriginalAssignedName;
                    var test12 = jsonType.OriginalName;
                    var test13 = jsonType.NewAssignedName;
                    var test14 = jsonType.Fields;
                    var test15 = jsonType.InternalType;
                    var test16 = jsonType.Type;
                    var test17 = jsonType.IsRoot;
                    var test18 = jsonType.IsVariant;
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
                orig.Fields.Count - dup.Fields.Count > differentOriginalNameSensitivity)
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
