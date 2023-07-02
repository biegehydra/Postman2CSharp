using System.Text.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Postman2CSharp.Core.Utilities;

namespace Postman2CSharp.Tests;

[TestClass]
public class HelpersTests
{
    private string ReadFile(string filePath)
    {
        var directory = Directory.GetCurrentDirectory().Replace("bin\\Debug\\net7.0", "");
        return File.ReadAllText(Path.Join(directory, filePath));
    }

    private T? ReadFileAsJson<T>(string filePath) where T : class
    {
        return JsonSerializer.Deserialize<T>(ReadFile(filePath));
    }

    [TestMethod]
    public void ConsolidateNamespaces()
    {
        var unconsolidatedNamespaces = ReadFile("MultipleNamespaces.txt");

        var consolidated = CodeAnalysisUtils.ConsolidateNamespaces(unconsolidatedNamespaces, "GeolocateRequest");

        var tree = CSharpSyntaxTree.ParseText(consolidated);
        var root = tree.GetCompilationUnitRoot();

        var namespacesCount = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Count();

        Assert.AreEqual(1, namespacesCount);
    }

    [TestMethod]
    public void NormalizeToCsharpPropertyName_GivenValidInput_ShouldReturnValidCSharpPropertyName()
    {
        var input = "valid_input";

        var actualResult = Utils.NormalizeToCsharpPropertyName(input);

        var expectedResult = "ValidInput";
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void NormalizeToCsharpPropertyName_WeirdInput()
    {
        var input = "invalid*&^%$^input";

        var actualResult = Utils.NormalizeToCsharpPropertyName(input);

        var expectedResult = "Invalidinput";
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void NormalizeToCsharpPropertyName_GivenValidInputWithPrivateAccess_ShouldReturnValidPrivateCSharpPropertyName()
    {
        var input = "valid_input";

        var actualResult = Utils.NormalizeToCsharpPropertyName(input, CsharpPropertyType.Private);

        var expectedResult = "_validInput";
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void NormalizeToCsharpPropertyName_GivenNullInput_ShouldReturnEmptyResult()
    {
        string? input = null;

        var actualResult = Utils.NormalizeToCsharpPropertyName(input);

        var expectedResult = string.Empty;
        Assert.AreEqual(expectedResult, actualResult);
    }

    [DataTestMethod]
    [DataRow("http://fulluri.com", "https://fulluri.com/path", "path")]
    [DataRow("https://fulluri.com/", "https://fulluri.com/path/", "path")]
    [DataRow("https://fulluri.com/", "https://fulluri.com/path1/path2", "path1/path2")]
    [DataRow("https://fulluri.com/", "https://fulluri.com/path1/{path2}", "path1/{path2}")]
    public void ExtractRelativePath_ValidInput_ReturnsExpected(string baseUri, string fullUri, string expected)
    {
        var actual = Utils.ExtractRelativePath(baseUri, fullUri);

        Assert.AreEqual(expected, actual);
    }

    [DataTestMethod]
    [DataRow(null, null, false)]
    [DataRow(null, "https://fulluri.com/path", false)]
    [DataRow("https://fulluri.com", null, false)]
    [DataRow("https://fulluri.com", "https://fulluri.com/path", false)]
    [DataRow("https://fulluri.com", "https://fulluri.com//path", false)]
    [DataRow("https://fulluri.com/path1", "https://baseuri.com/path2", true)]
    public void ExtractRelativePath_InvalidInput_ThrowsOrReturnsEmpty(string baseUri, string fullUri, bool expectException)
    {
        try
        {
            Utils.ExtractRelativePath(baseUri, fullUri);

            if (expectException)
            {
                Assert.Fail("Expected exception.");
            }
        }
        catch (ArgumentException)
        {
            if (!expectException)
            {
                Assert.Fail("Unexpected exception.");
            }
        }
    }

    [TestMethod]
    [DataRow(new[] { "https://www.google.com/search?q=postman", "https://www.google.com/search?q=postman+to+C%23+converter", "https://www.google.com/search?q=postman2CSharp" }, "https://www.google.com/search?q=postman")]
    [DataRow(new[] { "https://www.google.com/search?q=postman", "https://www.c-sharpcorner.com/", "https://www.google.com/search?q=postman+to+C%23+converter", "https://www.c-sharpcorner.com/11233/postman2CSharp" }, "https://www.")]
    [DataRow(new[] { "Justin", "Just", "Justine", "jus" }, null)]
    public void GetCommonBase_Should_Return_Expected_CommonBase(string[] strings, string expectedCommonBase)
    {
        var result = Utils.GetCommonBase(new List<string>(strings));
        Assert.AreEqual(expectedCommonBase, result);
    }

    [TestMethod]
    [DataRow(new string[] { })]
    [DataRow(new[] { "", " ", null })]
    [DataRow(new[] { "abc", "abcd" })]
    [DataRow(new[] { " 123", null, "123" })]
    public void GetCommonBase_Should_Return_Null(string[] strings)
    {
        var result = Utils.GetCommonBase(new List<string>(strings));
        Assert.IsNull(result);
    }

    [TestMethod]
    [DataRow(new string[] { "hello", "hi" }, null)]
    [DataRow(new string[] { "hello", "hell" }, null)]
    [DataRow(new string[] { "hello", "world" }, null)]
    [DataRow(new string[] { "HelloWorld", "Hello" }, "Hello")]
    [DataRow(new string[] { "ThisIsALongerString", "ThisIsLonger" }, "ThisIs")]
    [DataRow(new string[] { "ThisIsABiggerLongerString", "ThisIsABiggerLonger", "ThisIsABiggerLongerString123" }, "ThisIsABiggerLonger")]
    [DataRow(new string[] { "a", "a", "a", "a", "a" }, null)]
    [DataRow(new string[] { "hello", "", "world" }, null)]
    [DataRow(new string[] { "ThisIsABiggerLongerString", "ThisNotABiggerLonger", "ThisIsABiggerLongerString123" }, "ABiggerLonger")]
    public void GetLongestSubstring_ReturnsCorrectSubstring_WhenGivenListOfStrings(string[] strings, string expected)
    {
        var actual = Utils.GetLongestSubstring(strings.ToList());

        Assert.AreEqual(expected, actual);
    }
}