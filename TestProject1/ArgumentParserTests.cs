using ChecksumValidator.CLI;
using ChecksumValidator.CLI.Dtos;

namespace TestProject1;

public class ArgumentParserTests
{
    [Theory]
    [MemberData(nameof(TestData.ParsedArgumentsData), MemberType = typeof(TestData))]
    public  void ArgumentParserTest(string[] arguments, ParsedArgumentsDto expectedParsedDto)
    {
        //act
        var result = ArgumentParser.ParseArguments(arguments);
        //assert
        Assert.Equal(expectedParsedDto.FilePath, result.FilePath);
        Assert.Equal(expectedParsedDto.KnownHash, result.KnownHash);
        Assert.Equal(expectedParsedDto.SelectedAlgorithm, result.SelectedAlgorithm);
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidAlgorithmSelectionData), MemberType = typeof(TestData))]
    public void ArgumentParserTestWithInvalidArguments_ThrowsException(string[] arguments)
    {
        Action act = () => ArgumentParser.ParseArguments(arguments);
        Assert.Throws<InvalidAlgorithmException>(act);
    }
}