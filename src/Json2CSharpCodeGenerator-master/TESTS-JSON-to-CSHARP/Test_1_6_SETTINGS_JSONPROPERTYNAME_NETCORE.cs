using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xamasoft.JsonClassGenerator;
using Xamasoft.JsonClassGenerator.CodeWriterConfiguration;
using Xamasoft.JsonClassGenerator.CodeWriters;
using Xamasoft.JsonClassGenerator.Models;

namespace TESTS_JSON_TO_CSHARP
{
    [TestClass]
    public class Test_1_6_SETTINGS_JSONPROPERTYNAME_NETCORE
    {
        [TestMethod]
        public void JsonPropertyNameNetCore()
        {
            string path       = Directory.GetCurrentDirectory() + @"/Test_1_6_SETTINGS_JSONPROPERTYNAME_NETCORE_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory() + @"/Test_1_6_SETTINGS_JSONPROPERTYNAME_NETCORE_OUTPUT.txt";
            string input      = File.ReadAllText(path);

            CSharpCodeWriterConfig csharpCodeWriterConfig = new CSharpCodeWriterConfig();
            csharpCodeWriterConfig.AttributeLibrary = JsonLibrary.SystemTextJson;
            csharpCodeWriterConfig.UsePascalCase = true;
            csharpCodeWriterConfig.AttributeUsage = JsonPropertyAttributeUsage.Always;
            CSharpCodeWriter csharpCodeWriter = new CSharpCodeWriter(csharpCodeWriterConfig, false);

            JsonClassGenerator jsonClassGenerator = new JsonClassGenerator(csharpCodeWriter, new DuplicateOptions() { RemoveDuplicateRoots = false, RemoveSemiDuplicates = false });

            string returnVal = jsonClassGenerator.GenerateClasses(input, true, false, out string errorMessage).ToString();
            string resultsCompare = File.ReadAllText(resultPath);
            Assert.AreEqual(resultsCompare.NormalizeOutput(), returnVal.NormalizeOutput());
        }

        /// <summary>
        /// Fixes https://github.com/Json2CSharp/Json2CSharpCodeGenerator/issues/52
        /// </summary>
        [TestMethod]
        public void ImmutableClassesAndSystemTextJson()
        {
            string path       = Directory.GetCurrentDirectory() + @"Test_1_6_SETTINGS_JSONPROPERTYNAME_NETCORE_INPUT.txt";
            string resultPath = Directory.GetCurrentDirectory() + @"Test_1_6_SETTINGS_JSONPROPERTYNAME_NETCORE_OUTPUT1.txt";
            string input      = File.ReadAllText(path);

            CSharpCodeWriterConfig csharpCodeWriterConfig = new CSharpCodeWriterConfig();
            csharpCodeWriterConfig.AttributeLibrary = JsonLibrary.SystemTextJson;
            csharpCodeWriterConfig.OutputType = OutputTypes.ImmutableClass;
            csharpCodeWriterConfig.UsePascalCase = true;
            csharpCodeWriterConfig.AttributeUsage = JsonPropertyAttributeUsage.Always;
            CSharpCodeWriter csharpCodeWriter = new CSharpCodeWriter(csharpCodeWriterConfig, false);

            JsonClassGenerator jsonClassGenerator = new JsonClassGenerator(csharpCodeWriter, new DuplicateOptions() { RemoveDuplicateRoots = false, RemoveSemiDuplicates = false });

            string returnVal = jsonClassGenerator.GenerateClasses(input, true, false, out string errorMessage).ToString();
            string resultsCompare = File.ReadAllText(resultPath);
            Assert.AreEqual(resultsCompare.NormalizeOutput(), returnVal.NormalizeOutput());
        }


        [TestMethod]
        public void RegressionCreatedAtUnderscoreIssue()
        {
            string path       = Directory.GetCurrentDirectory() + @"Test_1_6_SETTINGS_JSONPROPERTYNAME_NETCORE_INPUT2.txt";
            string resultPath = Directory.GetCurrentDirectory() + @"Test_1_6_SETTINGS_JSONPROPERTYNAME_NETCORE_OUTPUT2.txt";
            string input      = File.ReadAllText(path);

            CSharpCodeWriterConfig csharpCodeWriterConfig = new CSharpCodeWriterConfig();
            csharpCodeWriterConfig.AttributeLibrary = JsonLibrary.SystemTextJson;
            csharpCodeWriterConfig.OutputType = OutputTypes.ImmutableClass;
            csharpCodeWriterConfig.AttributeUsage = JsonPropertyAttributeUsage.Always;
            CSharpCodeWriter csharpCodeWriter = new CSharpCodeWriter(csharpCodeWriterConfig, false);

            JsonClassGenerator jsonClassGenerator = new JsonClassGenerator(csharpCodeWriter, new DuplicateOptions() { RemoveDuplicateRoots = false, RemoveSemiDuplicates = false });

            string returnVal = jsonClassGenerator.GenerateClasses(input, true, false, out string errorMessage).ToString();
            string resultsCompare = File.ReadAllText(resultPath);
            Assert.AreEqual(resultsCompare.NormalizeOutput(), returnVal.NormalizeOutput());
        }

    }
}

