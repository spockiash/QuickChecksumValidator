using CLI_Tool.Dtos;
using CLI_Tool.Enums;
using CLI_Tool.Helpers;
using CommandLine;

namespace CLI_Tool;

public class ArgumentParser
{
    /// <summary>
    /// Constructs parser object that wraps command line parser provided by NuGet package.
    /// </summary>
    /// <param name="settings">Delegate that accepts parser settings in lambda form, for example: 'with => with.HelpWriter = null'</param>
    public ArgumentParser(Action<ParserSettings> settings)
    {
        Parser = new Parser(settings);
    }

    private static Parser? Parser { get; set; }
    
    public ParsedArgumentsDto ParseArguments(string[] args)
    {
        var parsedArguments = new ParsedArgumentsDto(null, null);
        var parsingResult = Parser?.ParseArguments<Options>(args);
        parsingResult.WithParsed(options =>
        {
            //set parsed values into dto
            parsedArguments.SetFilePath(options.FilePath);
            parsedArguments.SetKnownHash(options.KnownHash);
            //Parse provided string value into AlgoType enum and store into dto
            if (options.SelectedAlgorithm != null)
            {
                try
                {
                    parsedArguments.SetAlgorithm(ParseAlgorithm(options.SelectedAlgorithm));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    DisplayHelper.DisplaySupportedAlgorithms();
                    Environment.Exit(1); // Exit the program with an error code
                }
            }
            else
            {
                parsedArguments.SelectedAlgorithm = AlgoType.Md5;
            }
        }).WithNotParsed(errors =>
        {
            //Iterate over errors when present
            foreach (var error in errors)
            {
                if (error.Tag == ErrorType.HelpRequestedError || error.Tag == ErrorType.VersionRequestedError)
                {
                    if (parsingResult != null) DisplayHelper.DisplayHelp(parsingResult);
                }
                else
                {
                    
                    HandleGenericError(error);
                }
            }
        });
        
        return parsedArguments;
    }

    private void HandleGenericError(Error error)
    {
        switch (error)
        {
            //Handle bad token with unknown option
            case TokenError { Tag: ErrorType.UnknownOptionError } tokenError:
                DisplayHelper.DisplayUnknownOptionError(tokenError.Token);
                break;
            default:
                DisplayHelper.DisplayError(error.Tag.ToString());
                break;
        }
    }
    
    /// <summary>
    /// Converts the algorithm string to AlgoType, defaulting to MD5 if the input is null or invalid.
    /// </summary>
    public AlgoType ParseAlgorithm(string? algorithmString)
    {
        var success = Enum.TryParse(typeof(AlgoType), algorithmString, true, out var algoEnum);
        
        if (success)
        {
            // If parsing succeeds, cast and return the parsed enum value
            return (AlgoType)(algoEnum ?? AlgoType.Md5);
        }

        // If parsing fails, print the supported algorithms and return the default
        throw new InvalidAlgorithmException($"Invalid algorithm specified: {algorithmString ?? "null"}");
    }
}