namespace TestProject1;

using ChecksumValidator.CLI;
using ChecksumValidator.CLI.Enums;
using Xunit;

public class ArgumentParserTests
{
    private readonly ArgumentParser _argumentParser;

    public ArgumentParserTests()
    {
        // Initialize the parser with basic settings (null for this test)
        _argumentParser = new ArgumentParser(with => with.HelpWriter = null);
    }

    [Fact]
    public void ParseAlgorithm_ValidSHA256String_ReturnsSHA256Enum()
    {
        // Act
        var result = _argumentParser.ParseAlgorithm("SHA256");

        // Assert
        Assert.Equal(AlgoType.Sha256, result);
    }

    [Fact]
    public void ParseAlgorithm_ValidSHA1String_ReturnsSHA1Enum()
    {
        // Act
        var result = _argumentParser.ParseAlgorithm("SHA1");

        // Assert
        Assert.Equal(AlgoType.Sha1, result);
    }

    [Fact]
    public void ParseAlgorithm_ValidMD5String_ReturnsMD5Enum()
    {
        // Act
        var result = _argumentParser.ParseAlgorithm("MD5");

        // Assert
        Assert.Equal(AlgoType.Md5, result);
    }

    [Fact]
    public void ParseAlgorithm_InvalidString_ReturnsDefaultMD5Enum()
    {
        // Act
        var result = _argumentParser.ParseAlgorithm("invalidAlgo");

        // Assert
        Assert.Equal(AlgoType.Md5, result); // Default to MD5
    }

    [Fact]
    public void ParseAlgorithm_NullString_ReturnsDefaultMD5Enum()
    {
        // Act
        var result = _argumentParser.ParseAlgorithm(null);

        // Assert
        Assert.Equal(AlgoType.Md5, result); // Default to MD5
    }

    [Fact]
    public void ParseAlgorithm_EmptyString_ReturnsDefaultMD5Enum()
    {
        // Act
        var result = _argumentParser.ParseAlgorithm("");

        // Assert
        Assert.Equal(AlgoType.Md5, result); // Default to MD5
    }

    [Fact]
    public void ParseAlgorithm_ValidLowercaseString_ReturnsCorrectEnum()
    {
        // Act
        var result = _argumentParser.ParseAlgorithm("sha256");

        // Assert
        Assert.Equal(AlgoType.Sha256, result); // Should correctly parse case-insensitive input
    }

    [Fact]
    public void ParseAlgorithm_ValidMixedCaseString_ReturnsCorrectEnum()
    {
        // Act
        var result = _argumentParser.ParseAlgorithm("Md5");

        // Assert
        Assert.Equal(AlgoType.Md5, result); // Should correctly parse mixed-case input
    }
}
