using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ObjectInitializerGenerator
{
    public class Generator
    {
        private readonly ICodeWriter codeWriter;
        List<ObjectModel> objectModelList;
        public Generator(ICodeWriter codeWriter)
        {
            this.codeWriter = codeWriter;
        }

        public Generator Analyse(string input)
        {
            try
            {
                SyntaxTree tree = CSharpSyntaxTree.ParseText(input);
                CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

                objectModelList = new List<ObjectModel>();

                foreach (SyntaxNode child in root.ChildNodes())
                {
                    if (child.IsKind(SyntaxKind.ClassDeclaration))
                    {
                        ProcessClassNode(child);
                    }
                    else if (child.IsKind(SyntaxKind.NamespaceDeclaration))
                    {
                        foreach (SyntaxNode namespaceChild in child.ChildNodes())
                        {
                            if (namespaceChild.IsKind(SyntaxKind.ClassDeclaration))
                            {
                                ProcessClassNode(namespaceChild);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return this;
        }

        private void ProcessClassNode(SyntaxNode node)
        {
            ObjectModel parent = new ObjectModel();
            foreach (SyntaxToken item in node.ChildTokens())
            {
                if (item.IsKind(SyntaxKind.IdentifierToken))
                {
                    parent.TokenType = SyntaxKind.ClassDeclaration;
                    parent.SyntaxName = item.Value.ToString();
                    break;
                }
            }

            if (node.ChildNodes().Any())
            {
                AnalyzeChildNodes(node, parent);
            }

            objectModelList.Add(parent);
        }


        private void AnalyzeChildNodes(SyntaxNode node, ObjectModel parent)
        {

            foreach (SyntaxNode child in node.ChildNodes())
            {
                if (child.IsKind(SyntaxKind.ClassDeclaration))
                {
                    ObjectModel subParent = new ObjectModel();
                    foreach (SyntaxToken item in child.ChildTokens())
                    {
                        if (item.IsKind(SyntaxKind.IdentifierToken))
                        {
                            subParent.TokenType = SyntaxKind.ClassDeclaration;
                            subParent.SyntaxName = item.Value.ToString();
                            break;
                        }
                    }

                    if (child.ChildNodes().Any())
                    {
                        AnalyzeChildNodes(child, subParent);
                    }

                    parent.Children.Add(subParent);
                }
                else if (child.IsKind(SyntaxKind.PropertyDeclaration))
                {
                    ObjectModel property = new ObjectModel();
                    foreach (SyntaxToken item in child.ChildTokens())
                    {
                        if (item.IsKind(SyntaxKind.IdentifierToken))
                        {
                            property.TokenType = SyntaxKind.PropertyDeclaration;
                            property.SyntaxName = item.Value.ToString();
                            break;
                        }
                    }

                    foreach (SyntaxNode item in child.ChildNodes())
                    {
                        if (item.IsKind(SyntaxKind.PredefinedType) // int, bool, string
                            || item.IsKind(SyntaxKind.NullableType) // Nullable kinds
                            || item.IsKind(SyntaxKind.IdentifierName)) // Classes, Guid, Boolean, String
                        {
                            property.PropertyType = item.ToString();
                            property.NodeType = item.Kind();
                            break;
                        }

                        if (item.IsKind(SyntaxKind.GenericName) // Lists, Dictionaries
                            || item.IsKind(SyntaxKind.ArrayType)) // Arrays
                        {
                            // get the generic type inside the array, or list
                            foreach (var genericItem in item.ChildNodes())
                            {
                                if (genericItem.IsKind(SyntaxKind.PredefinedType))
                                {
                                    property.GenericType = genericItem.ToString();
                                    break;
                                }
                            }
                            property.PropertyType = item.ToString();
                            property.NodeType = item.Kind();
                            break;
                        }
                    }
                    parent.Children.Add(property);
                }
            }
        }

        public string Write(string objectType)
        {
            var objectModel = objectModelList.Where(x => x.SyntaxName == objectType).ToList();
            try
            {
                return codeWriter.Write(objectModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}