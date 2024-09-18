using CLI_Tool.Enums;

namespace CLI_Tool.Dto;

public class ValidationResultDto(bool validationSuccess, string pathToFile, string knownHash, string calculatedHash, AlgoType usedAlgoType)
{
    public bool ValidationSuccess { get; set; } = validationSuccess;
    public string PathToFile { get; set; } = pathToFile;
    public string KnownHash { get; set; } = knownHash;
    public string CalculatedHash { get; set; } = calculatedHash;
    public AlgoType UsedAlgorithm { get; set; } = usedAlgoType;
}