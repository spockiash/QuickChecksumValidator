// See https://aka.ms/new-console-template for more information

using ChecksumValidator.CLI;
using ChecksumValidator.CLI.Dtos;
using ChecksumValidator.CLI.Helpers;

Initialize(args);
PerformHash();

public static partial class Program
{
    private static ArgumentParser? ArgParser { get; set; }
    private static ParsedArgumentsDto? ParsedArguments { get; set; }
    private static ChecksumProvider? ChecksumProvider { get; set; }
    private static void Initialize(string[] args)
    {
        ArgParser = new ArgumentParser(with => with.HelpWriter = null);
        ParsedArguments = ArgParser.ParseArguments(args);
    }

    private static void PerformHash()
    {
        ChecksumProvider = new ChecksumProvider();
        if (ParsedArguments != null && ValidateParsedArgumentsDto(ParsedArguments))
        {
            try
            {
                var result = ChecksumProvider.ValidateIntegrity(ParsedArguments);
                DisplayHelper.DisplayResult(result);
            }
            catch (FileNotFoundException e)
            {
                DisplayHelper.DisplayError(e.Message);
                Environment.Exit(1);
            }
            
            
        }
    }

    private static bool ValidateParsedArgumentsDto(ParsedArgumentsDto parsedArguments) =>
        !string.IsNullOrEmpty(parsedArguments.FilePath) && !string.IsNullOrEmpty(parsedArguments.KnownHash);
}