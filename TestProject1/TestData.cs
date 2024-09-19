using ChecksumValidator.CLI.Dtos;
using ChecksumValidator.CLI.Enums;

namespace TestProject1;

public class TestData
{
    public static IEnumerable<object[]> ParsedArgumentsData()
    {
        yield return new object[] { new string[] { "pathToFile" }, new ParsedArgumentsDto("", "") };
        yield return new object[] { new string[] { "anotherPath abc" }, new ParsedArgumentsDto("", "") };
        yield return new object[] { new string[] { "anotherPathToFile", "hashExample" }, new ParsedArgumentsDto("anotherPathToFile", "hashExample") };
    }
    
    public static IEnumerable<object[]> InvalidAlgorithmSelectionData()
    {
        yield return new object[] { new string[] { "path", "hash", "-a", "nonAvailableAlgo"} };
    }
}