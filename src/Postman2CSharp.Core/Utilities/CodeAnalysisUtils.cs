using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace Postman2CSharp.Core.Utilities
{
    public static class CodeAnalysisUtils
    {
        public static string ConsolidateNamespaces(string sourceCode)
        {
            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = tree.GetCompilationUnitRoot();

            var namespaces = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().GroupBy(n => n.Name.ToString());

            // Create a new root without any namespace
            var newRoot = SyntaxFactory.CompilationUnit()
                .WithExterns(root.Externs)
                .WithUsings(root.Usings)
                .WithAttributeLists(root.AttributeLists)
                .WithLeadingTrivia(root.GetLeadingTrivia())
                .WithTrailingTrivia(root.GetTrailingTrivia())
                .WithMembers(new SyntaxList<MemberDeclarationSyntax>());

            foreach (var namespaceGroup in namespaces)
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

        public static string ReorderClasses(string sourceCode, string rootName)
        {
            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = tree.GetRoot();

            var namespaceDeclaration = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().First();

            var classes = namespaceDeclaration.Members.OfType<ClassDeclarationSyntax>().ToList();
            var orderedClasses = classes
                .OrderBy(c => c.Identifier.Text != rootName) // RootName class first
                .ThenByDescending(c => c.Members.OfType<PropertyDeclarationSyntax>().Count() + c.Members.OfType<FieldDeclarationSyntax>().Count()); // Then order by property and field count

            var newNamespaceDeclaration = namespaceDeclaration.RemoveNodes(classes, SyntaxRemoveOptions.KeepNoTrivia);

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
}
