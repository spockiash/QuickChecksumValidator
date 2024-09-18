using CommandLine;

namespace CLI_Tool;

public class Options
{
    // C# attributes require constant expressions for their parameters, determined at compile time.
    // Resource (.resx) values are loaded at runtime and cannot be used in attributes, as attributes
    // are embedded into the assembly's metadata. Instead, placeholders or literals must be used, 
    // with resource values applied dynamically at runtime if needed.

    [Value(0, Required = true, HelpText = "Path to the file that needs to be verified.")]
    public string? FilePath { get; set; }
    [Value(1, Required = true, HelpText = "Known checksum hash.")]
    public string? KnownHash { get; set; }
    
    [Option('a', "algorithm", Required = false, HelpText = "Input supported algorithm type.")]
    public string? SelectedAlgorithm { get; set; }
}