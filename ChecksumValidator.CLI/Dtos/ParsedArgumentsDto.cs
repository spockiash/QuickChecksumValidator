using CLI_Tool.Enums;

namespace CLI_Tool.Dtos;

public class ParsedArgumentsDto(string? filePath, string? knownHash, AlgoType algoType = AlgoType.Md5)
{
    public string FilePath { get; set; } = filePath ?? string.Empty;
    public string KnownHash { get; set; } = knownHash ?? string.Empty;
    public AlgoType SelectedAlgorithm { get; set; } = algoType;
    
    public void SetFilePath(string? filePath)
    {
        FilePath = filePath ?? string.Empty;
    }
    public void SetKnownHash(string? knownHash)
    {
        KnownHash = knownHash ?? string.Empty;
    }
    public void SetAlgorithm(AlgoType selectedAlgorithm)
    {
        SelectedAlgorithm = selectedAlgorithm;
    }
}