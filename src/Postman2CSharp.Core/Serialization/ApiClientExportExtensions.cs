using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Postman2CSharp.Core.Core;
using Postman2CSharp.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Postman2CSharp.Core.Serialization
{
    public static class ApiClientExportExtensions
    {
        private static readonly List<string> HttpExtensionsNamespaces = new () { "System.Text", "System.Text.Json" };
        private static readonly List<string> QueryHelperNamespaces = new () { "System.Text", "System.Text.Encodings.Web" };

        public static Dictionary<string, (string? HttpCall, string SourceCode)>? ExportToDict(this ApiClient? apiClient)
        {
            var dict = new Dictionary<string, (string? HttpCall, string SourceCode)>();
            if (apiClient == null) return null;

            foreach (var httpCall in apiClient.HttpCalls)
            {

                httpCall.QueryParameterSourceCode = AddInterface(httpCall.QueryParameterSourceCode, httpCall.QueryParameterTypes, httpCall.QueryParameterClassName, Consts.Interfaces.IQueryParameters);
                AddToDictionary(httpCall.FormDataSourceCode, httpCall.FormDataClassName, httpCall.Name);
                AddToDictionary(httpCall.ResponseSourceCode, httpCall.ResponseClassName, httpCall.Name);
                AddToDictionary(httpCall.RequestSourceCode, httpCall.RequestClassName, httpCall.Name);
                AddToDictionary(httpCall.QueryParameterSourceCode, httpCall.QueryParameterClassName, httpCall.Name);
            }

            var apiClientSc = AddNamespaceAndUsingsToSourceCode(apiClient.NameSpace, apiClient.SourceCode, true, apiClient.NameSpaces());
            var oauth2QueryParamsSc = AddNamespaceAndUsingsToSourceCode(apiClient.NameSpace, CoreCsFile.OAuth2QueryParameters, true);
            var helperExtensionsSc = AddNamespaceAndUsingsToSourceCode(apiClient.NameSpace, CoreCsFile.HelperExtensions, true);
            var interfacesSc = AddNamespaceAndUsingsToSourceCode(apiClient.NameSpace, CoreCsFile.Interfaces);
            var queryHelpersSc = AddNamespaceAndUsingsToSourceCode(apiClient.NameSpace, CoreCsFile.QueryHelpers, true, QueryHelperNamespaces);
            var httpExtensions = AddNamespaceAndUsingsToSourceCode(apiClient.NameSpace, CoreCsFile.HttpExtensions, true, HttpExtensionsNamespaces);

            AddToDictionary(apiClientSc, apiClient.Name);
            AddToDictionary(apiClient.InterfaceSourceCode, apiClient.InterfaceName);
            AddToDictionary(oauth2QueryParamsSc, nameof(CoreCsFile.OAuth2QueryParameters));
            AddToDictionary(helperExtensionsSc, nameof(CoreCsFile.HelperExtensions));
            AddToDictionary(interfacesSc, nameof(CoreCsFile.Interfaces));
            AddToDictionary(queryHelpersSc, nameof(CoreCsFile.QueryHelpers));
            AddToDictionary(httpExtensions, nameof(CoreCsFile.HttpExtensions));

            foreach (var (fileName, detail) in dict)
            {
                dict[fileName] = new (detail.HttpCall, NormalizeWhitespace(detail.SourceCode));
            }

            return dict;

            void AddToDictionary(string? sourceCode, string? fileNameNoExtension, string? httpCallName = null)
            {
                if (sourceCode == null || fileNameNoExtension == null) return;
                var fullFileName = fileNameNoExtension + ".cs";
                if (fullFileName == ".cs") return;
                dict!.Add(fullFileName, (httpCallName, sourceCode));
            }
        }

        public static async Task ExportToFolder(this ApiClient? apiClient, string folderPath)
        {
            var srcCodeDict = apiClient?.ExportToDict();
            if (srcCodeDict == null) return;

            foreach (var (fileName, detail) in srcCodeDict)
            {
                if (fileName.Contains(nameof(Consts.Request)) || fileName.Contains(nameof(Consts.Response))
                || fileName.Contains(nameof(Consts.QueryParameters)) || fileName.Contains(nameof(Consts.FormData)))
                {
                    if (detail.HttpCall == null) throw new NullReferenceException(nameof(detail.HttpCall));
                    var fullPath = Path.Combine(folderPath, detail.HttpCall);
                    await CreateCsFile(detail.SourceCode, fullPath, fileName);
                }
                else
                {
                    await CreateCsFile(detail.SourceCode, folderPath, fileName);
                }

            }
            static async Task CreateCsFile(string? sourceCode, string folderPath, string? fileName)
            {
                if (sourceCode == null || fileName == null) return;
                await File.WriteAllTextAsync(folderPath, sourceCode);
            }
        }

        private static string? AddNamespaceAndUsingsToSourceCode(string nameSpace, string? sourceCode, bool padLeft = false, List<string>? namespaces = null)
        {
            if (string.IsNullOrWhiteSpace(sourceCode))
                return null;

            var sb = new StringBuilder();
            foreach (var ns in namespaces ?? new())
            {
                sb.Append($"using {ns};\n");
            }
            sb.Append($"namespace {nameSpace}\n");
            sb.Append("{\n");
            sb.Append(padLeft ? Helpers.PadLeft(sourceCode) : sourceCode);
            sb.Append("}\n");
            return sb.ToString();
        }

        private static string? AddInterface(string? input, List<ClassType>? classTypes, string? className, string interfaceName)
        {
            if (input == null || classTypes == null || className == null) return input;
            switch (interfaceName)
            {
                case Consts.Interfaces.IQueryParameters:
                {
                    if (input.Contains(Consts.Interfaces.IQueryParameters)) return input;

                    SyntaxTree tree = CSharpSyntaxTree.ParseText(input);
                    var root = (CompilationUnitSyntax)tree.GetRoot();

                    var rootType = classTypes.FirstOrDefault(x => x.AssignedName == className);
                    // Visit all nodes in the syntax tree
                    var newRoot = root.ReplaceNodes(root.DescendantNodes().OfType<ClassDeclarationSyntax>(),
                        (node, _) =>
                        {
                            if (node.Identifier.Text == className)
                            {
                                // Add the interface
                                node = node.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(Consts.Interfaces.IQueryParameters)));

                                var props = node.DescendantNodes().OfType<PropertyDeclarationSyntax>().ToList();
                                var propTypes = props
                                    .ToDictionary(x => x, x => rootType?.Fields
                                        .FirstOrDefault(y => y.MemberName.ToString().ToLower().Replace("_", string.Empty) == x.Identifier.Text.ToLower())?.JsonMemberName);

                                // Create method
                                var toDictionaryMethod = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("Dictionary<string, string?>"), "ToDictionary")
                                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                                    .WithBody(SyntaxFactory.Block(SyntaxFactory.ParseStatement("return new Dictionary<string, string?>\n{\n" +
                                        string.Join(",\n", node.DescendantNodes().OfType<PropertyDeclarationSyntax>()
                                            .Select(prop => $"{{ \"{propTypes[prop]}\", {prop.Identifier.Text} }}")) +
                                        "\n};")));

                                // Add the dictionary method
                                node = node.AddMembers(toDictionaryMethod);
                            }

                            return node;
                        });

                    // This drove me insane. I couldn't figure out how to get the newlines to work properly.
                    return newRoot.NormalizeWhitespace().ToFullString().FixXmlCommentsAfterCodeAnalysis();
                }
                case "IFormData": break;
                case "IMultipartFormData": break;
            }

            return input;
        }

        private static string NormalizeWhitespace(string sourceCode)
        {
            sourceCode = sourceCode.TrimEnd();

            sourceCode = sourceCode.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
            return sourceCode;
        }
    }
}
