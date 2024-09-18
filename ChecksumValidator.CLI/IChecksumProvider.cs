using CLI_Tool.Dto;
using CLI_Tool.Dtos;

namespace CLI_Tool;

public interface IChecksumProvider
{
    /// <summary>
    /// Validates the integrity of the file and compares it against provided checksum.
    /// </summary>
    /// <param name="parsedArguments">Object containing path, known checksum and selected algorithm</param>
    /// <returns></returns>
    ValidationResultDto ValidateIntegrity(ParsedArgumentsDto parsedArguments);
}