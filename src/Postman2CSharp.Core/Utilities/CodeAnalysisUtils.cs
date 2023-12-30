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

        public static string ConsolidateAndReorder(string sourceCode, string rootClassName, out int classCount)
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

            var consolidatedNs = newRoot.DescendantNodes().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();

            if (consolidatedNs == null)
            {
#if DEBUG
                Debug.WriteLine($"No classes generated for {rootClassName}");
#endif
                throw new NoClassesGeneratedException()
                {
                    IntendedRootName = rootClassName
                };
            }

            var classes = consolidatedNs.Members.OfType<ClassDeclarationSyntax>().ToList();
            classCount = classes.Count;
            if (classCount == 0)
            {
#if DEBUG
                Debug.WriteLine($"No classes generated for {rootClassName}");
#endif
                throw new NoClassesGeneratedException()
                {
                    IntendedRootName = rootClassName
                };
            }
            var orderedClasses = classes
                .OrderBy(c => c.Identifier.Text != rootClassName) // RootName class first
                .ThenByDescending(c =>
                    c.Members.OfType<PropertyDeclarationSyntax>().Count() +
                    c.Members.OfType<FieldDeclarationSyntax>().Count()); // Then order by property and field count

            var newNamespaceDeclaration =
                consolidatedNs.RemoveNodes(classes, SyntaxRemoveOptions.KeepNoTrivia);

            // Add the ordered classes to the namespace.
            newNamespaceDeclaration = newNamespaceDeclaration?.AddMembers(orderedClasses.ToArray());

            if (newNamespaceDeclaration == null)
            {
                return string.Empty;
            }

            newRoot = newRoot.ReplaceNode(consolidatedNs, newNamespaceDeclaration);

            return newRoot.NormalizeWhitespace().ToFullString();
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

            return newRoot.NormalizeWhitespace().ToFullString();
        }

        public static SyntaxNode CarefullyRemoveAsyncAwait(SyntaxNode root)
        {
            var methodDeclarations = root.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();

            for (int i = 0; i < methodDeclarations.Count(); i++)
            {
                var method = methodDeclarations[i];
                // Analyze the method to see if async/await can be safely removed
                if (CanRemoveAsyncAwaitSafely(method))
                {
                    // Create a new method declaration without 'async' and 'await'
                    var newMethod = method.WithoutAsyncAndAwait();

                    // Replace old method with new one
                    root = root.ReplaceNode(method, newMethod);

                    // idk why but if I don't do this it only removes the first async/await
                    methodDeclarations = root.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();
                }
            }

            return root;
        }

        private static bool CanRemoveAsyncAwaitSafely(MethodDeclarationSyntax method)
        {
            // Check if the method is async
            if (!method.Modifiers.Any(SyntaxKind.AsyncKeyword))
            {
                return false;
            }

            // Get all return statements in the method
            var returnStatements = method.DescendantNodes().OfType<ReturnStatementSyntax>();

            // Check if there is exactly one return statement
            if (returnStatements.Count() != 1)
            {
                return false;
            }

            // Check if there is exactly one 'await' expression in the method
            var awaitExpressions = method.DescendantNodes().OfType<AwaitExpressionSyntax>();
            if (awaitExpressions.Count() != 1)
            {
                return false;
            }

            // Check if the return statement contains an await expression
            var returnStatement = returnStatements.First();
            return returnStatement.Expression is AwaitExpressionSyntax;
        }

        private static MethodDeclarationSyntax WithoutAsyncAndAwait(this MethodDeclarationSyntax method)
        {
            // Find the 'async' keyword token
            var asyncKeyword = method.Modifiers.First(modifier => modifier.IsKind(SyntaxKind.AsyncKeyword));

            // Remove the 'async' keyword
            var newModifiers = method.Modifiers.Remove(asyncKeyword);

            // Replace the await expression with its operand in the return statement
            var returnStatement = method.DescendantNodes().OfType<ReturnStatementSyntax>().First();
            var newReturnStatement = returnStatement.WithExpression(((AwaitExpressionSyntax)returnStatement.Expression!).Expression);

            // Replace the old return statement with the new one
            var newBody = method.Body?.ReplaceNode(returnStatement, newReturnStatement);

            // Return the new method declaration
            return method.WithModifiers(newModifiers).WithBody(newBody);
        }

        public static string ReorderClassesNoNamespace(string sourceCode, string rootName, out int classCount)
        {
            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = tree.GetRoot() as CompilationUnitSyntax;

            var classes = root!.DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();
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

            var newRoot = SyntaxFactory.CompilationUnit();

            foreach (var c in orderedClasses)
            {
                newRoot = newRoot.AddMembers(c);
            }

            return newRoot.NormalizeWhitespace().ToFullString();
        }
    }

    public class NoClassesGeneratedException : Exception
    {
        public required string IntendedRootName { get; set; }
    }
}
