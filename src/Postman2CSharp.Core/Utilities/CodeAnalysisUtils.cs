using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace Postman2CSharp.Core.Utilities
{
    public static class CodeAnalysisUtils
    {
        public static string? ExtractClassDeclaration(this string sourceCode, string className)
        {
            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = tree.GetCompilationUnitRoot();

            var classDeclaration = root.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault(c => c.Identifier.Text == className);

            return classDeclaration?.NormalizeWhitespace().ToFullString();
        }

        public static string ConsolidateNamespaces(string sourceCode, string rootClassName)
        {
            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = tree.GetCompilationUnitRoot();

            var namespaces = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().ToList();
            if (!namespaces.Any())
            {
#if DEBUG
                Debug.WriteLine($"No classes generated.");
#endif
                throw new NoClassesGeneratedException()
                {
                    IntendedRootName = rootClassName
                };
            }
            var namespaceGroups = namespaces.GroupBy(n => n.Name.ToString());

            // Create a new root without any namespace
            var newRoot = SyntaxFactory.CompilationUnit()
                .WithExterns(root.Externs)
                .WithUsings(root.Usings)
                .WithAttributeLists(root.AttributeLists)
                .WithLeadingTrivia(root.GetLeadingTrivia())
                .WithTrailingTrivia(root.GetTrailingTrivia())
                .WithMembers(new SyntaxList<MemberDeclarationSyntax>());

            foreach (var namespaceGroup in namespaceGroups)
            {
                var consolidatedNamespace = namespaceGroup.First();

                // Add all members from duplicate namespaces to the first one
                foreach (var duplicateNamespace in namespaceGroup.Skip(1))
                {
                    consolidatedNamespace = consolidatedNamespace.AddMembers(duplicateNamespace.Members.ToArray());
                }

                // Add consolidated namespace to the new root
                newRoot = newRoot.AddMembers(consolidatedNamespace);
            }

            return newRoot.NormalizeWhitespace().ToFullString().FixXmlCommentsAfterCodeAnalysis(2);
        }

        public static string ReorderClasses(string sourceCode, string rootName, out int classCount)
        {
            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = tree.GetRoot();

            var namespaces = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>();
            var namespaceDeclaration = namespaces.FirstOrDefault();
            if (namespaceDeclaration == null)
            {
#if DEBUG
                Debug.WriteLine($"No classes generated for {rootName}");
#endif
                throw new NoClassesGeneratedException()
                {
                    IntendedRootName = rootName
                };
            }
            var classes = namespaceDeclaration.Members.OfType<ClassDeclarationSyntax>().ToList();
            classCount = classes.Count;
            if (classCount == 0)
            {
#if DEBUG
                Debug.WriteLine($"No classes generated for {rootName}");
#endif
                throw new NoClassesGeneratedException()
                {
                    IntendedRootName = rootName
                };
            }
            var orderedClasses = classes
                .OrderBy(c => c.Identifier.Text != rootName) // RootName class first
                .ThenByDescending(c =>
                    c.Members.OfType<PropertyDeclarationSyntax>().Count() +
                    c.Members.OfType<FieldDeclarationSyntax>().Count()); // Then order by property and field count

            var newNamespaceDeclaration =
                namespaceDeclaration.RemoveNodes(classes, SyntaxRemoveOptions.KeepNoTrivia);

            // Add the ordered classes to the namespace.
            newNamespaceDeclaration = newNamespaceDeclaration?.AddMembers(orderedClasses.ToArray());

            if (newNamespaceDeclaration == null)
            {
                return string.Empty;
            }

            root = root.ReplaceNode(namespaceDeclaration, newNamespaceDeclaration);

            return root.NormalizeWhitespace().ToFullString().FixXmlCommentsAfterCodeAnalysis(2);
        }
    }

    public class NoClassesGeneratedException : Exception
    {
        public required string IntendedRootName { get; set; }
    }
}
