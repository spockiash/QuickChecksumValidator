using ChecksumValidator.CLI;
using ChecksumValidator.CLI.Dtos;

namespace TestProject1;

public class ArgumentParserTests
{
    [Theory]
    [MemberData(nameof(TestData.ParsedArgumentsData), MemberType = typeof(TestData))]
    public  void ArgumentParserTest(string[] arguments, ParsedArgumentsDto expectedParsedDto)
    {
        ArgumentParser.InitiateParser(with => with.HelpWriter = null);
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
        ArgumentParser.InitiateParser(with => with.HelpWriter = null);
        Action act = () => ArgumentParser.ParseArguments(arguments);
        Assert.Throws<InvalidAlgorithmException>(act);
    }
}