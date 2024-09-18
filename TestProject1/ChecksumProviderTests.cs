using ChecksumValidator.CLI;
using ChecksumValidator.CLI.Dtos;
using ChecksumValidator.CLI.Enums;

namespace TestProject1;

public class ChecksumProviderTests
{
    [Theory]
    [InlineData("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855", AlgoType.Sha256, true)]
    [InlineData("invalidchecksum", AlgoType.Sha256, false)]
    [InlineData("d41d8cd98f00b204e9800998ecf8427e", AlgoType.Md5, true)]
    [InlineData("da39a3ee5e6b4b0d3255bfef95601890afd80709", AlgoType.Sha1, true)]
    public void EmptyFileChecksumProviderTest(string checksum, AlgoType algorithm, bool expectedResult)
    {
        //Arrange
        // Create an empty temp file
        var tempFilePath = Path.GetTempFileName();
        var argumentsDto = new ParsedArgumentsDto(tempFilePath, checksum, algorithm);
        var checksumProvider = new ChecksumProvider();
        // Act
        var result = checksumProvider.ValidateIntegrity(argumentsDto);
        //Assert
        Assert.Equal(expectedResult, result.ValidationSuccess);
        // Clean up temp file
        File.Delete(tempFilePath);
    }
}