using Postman2CSharp.Core.Models.PostmanCollection;
using Postman2CSharp.Core.Utilities;

namespace Postman2CSharp.Tests;

[TestClass]
public class VariableExtractorTests
{
    [DataTestMethod]
    [DataRow("{{Hello}}", "Hello", "World", "World")]
    [DataRow("https://{{authority}}.com", "authority", "www.example", "https://www.example.com")]
    public void ReplaceVariableUsagesInVariableUsagesTest(string variableValue, string expectedVariableKey, string replacementValue, string expectedOutput)
    {
        // Arrange
        var collectionVariables = new List<CollectionVariable>
        {
            new CollectionVariable { Key = expectedVariableKey, Value = replacementValue }
        };

        var testCollectionVariable = new CollectionVariable { Key = "TestKey", Value = variableValue };
        collectionVariables.Add(testCollectionVariable);

        // Act
        VariableExtractor.ReplaceVariableUsagesInVariableUsages(collectionVariables);

        // Assert
        Assert.AreEqual(expectedOutput, testCollectionVariable.Value);
    }

    [DataTestMethod]
    [DataRow(null, null, null, null, DisplayName = "When input is null, output should be null")]
    [DataRow("{{Hello}}", "Hello", "World", "World")]
    [DataRow("https://{{authority}}.com", "authority", "www.example", "https://www.example.com")]
    public void ReplaceVariablesWithValuesTest(string? input, string? variableKey, string? variableValue, string? expectedOutput)
    {
        // Arrange
        var collectionVariables = new List<CollectionVariable>
        {
            new CollectionVariable { Key = variableKey!, Value = variableValue! }
        };

        // Act
        var result = VariableExtractor.ReplaceVariablesWithValues(input, collectionVariables);

        // Assert
        Assert.AreEqual(expectedOutput, result);
    }

    [DataTestMethod]
    [DataRow(null, null, null, null, DisplayName = "When input is null, output should be null")]
    [DataRow("{{Hello}}", "Hello", "_hello", "{_hello}")]
    [DataRow("https://{{authority}}.com", "authority", "_authority", "https://{_authority}.com")]
    public void ExtractAndReplaceVariablesTest(string? input, string? expectedOriginal, string? expectedCsPropertyUsage, string? expectedOutput)
    {
        // Act
        var (stringWithCSharpInterpolation, variables) = VariableExtractor.ExtractAndReplaceVariables(input);

        // Assert
        Assert.AreEqual(expectedOutput, stringWithCSharpInterpolation);
        if (expectedOriginal != null && expectedCsPropertyUsage != null)
        {
            Assert.IsTrue(variables.Any(v => v.Original == expectedOriginal && v.CSPropertyUsage == expectedCsPropertyUsage));
        }
    }
}