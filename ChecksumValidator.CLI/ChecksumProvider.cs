using System.Security.Cryptography;
using System.Text;
using ChecksumValidator.CLI.Dto;
using ChecksumValidator.CLI.Dtos;
using ChecksumValidator.CLI.Enums;

namespace ChecksumValidator.CLI;

public class ChecksumProvider : IChecksumProvider
{
    /// <summary>
    /// Validates the integrity of the file and compares it against provided checksum.
    /// </summary>
    /// <param name="parsedArguments"></param>
    /// <returns></returns>
    public ValidationResultDto ValidateIntegrity(ParsedArgumentsDto parsedArguments)
    {
        var computedChecksum = ComputeHash(parsedArguments.FilePath, parsedArguments.SelectedAlgorithm);
        var validationSuccess = computedChecksum.Equals(parsedArguments.KnownHash, StringComparison.OrdinalIgnoreCase);
        return new ValidationResultDto(validationSuccess, parsedArguments.FilePath,
            parsedArguments.KnownHash, computedChecksum, parsedArguments.SelectedAlgorithm);
    }

    private string ComputeHash(string path, AlgoType algorithmType)
    {
        try
        {
            using var fileStream = File.OpenRead(path);
            using var hashAlgorithm = CreateHashAlgorithm(algorithmType);
            if (hashAlgorithm == null)
                throw new ArgumentException("Invalid hashing algorithm specified.");

            var hashedBytes = hashAlgorithm.ComputeHash(fileStream);

            // Convert byte array to a string
            var builder = new StringBuilder();
            foreach (var b in hashedBytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
        catch (FileNotFoundException e)
        {
            // Rethrow the exception to be handled outside
            throw new FileNotFoundException("File not found.", e);
        }
    }

    private static HashAlgorithm? CreateHashAlgorithm(AlgoType algorithmType)
    {
        return algorithmType switch
        {
            AlgoType.Sha256 => SHA256.Create(),
            AlgoType.Sha1 => SHA1.Create(),
            AlgoType.Md5 => MD5.Create(),
            _ => null
        };
    }
}