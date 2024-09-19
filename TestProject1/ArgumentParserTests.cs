using System.Runtime.InteropServices;
using ChecksumValidator.CLI;
using ChecksumValidator.CLI.Dtos;
using ChecksumValidator.CLI.Enums;

namespace TestProject1;

public class ArgumentParserTests
{
    private ArgumentParser _parser = new ArgumentParser(with => with.HelpWriter = null);
    [Theory]
    [MemberData(nameof(TestData.ParsedArgumentsData), MemberType = typeof(TestData))]
    public  void ArgumentParserTest(string[] arguments, ParsedArgumentsDto expectedParsedDto)
    {
        //act
        var result = _parser.ParseArguments(arguments);
        //assert
        Assert.Equal(expectedParsedDto.FilePath, result.FilePath);
        Assert.Equal(expectedParsedDto.KnownHash, result.KnownHash);
        Assert.Equal(expectedParsedDto.SelectedAlgorithm, result.SelectedAlgorithm);
    }

    [Theory]
    [MemberData(nameof(TestData.InvalidAlgorithmSelectionData), MemberType = typeof(TestData))]
    public void ArgumentParserTestWithInvalidArguments_ThrowsException(string[] arguments)
    {
        Action act = () => _parser.ParseArguments(arguments);
        Assert.Throws<InvalidAlgorithmException>(act);
    }
}