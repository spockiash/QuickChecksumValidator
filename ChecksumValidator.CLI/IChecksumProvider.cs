using ChecksumValidator.CLI.Dto;
using ChecksumValidator.CLI.Dtos;

namespace ChecksumValidator;

public interface IChecksumProvider
{
    /// <summary>
    /// Validates the integrity of the file and compares it against provided checksum.
    /// </summary>
    /// <param name="parsedArguments">Object containing path, known checksum and selected algorithm</param>
    /// <returns></returns>
    ValidationResultDto ValidateIntegrity(ParsedArgumentsDto parsedArguments);
}