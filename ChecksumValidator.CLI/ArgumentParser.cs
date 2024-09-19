using ChecksumValidator.CLI.Dtos;
using ChecksumValidator.CLI.Enums;
using ChecksumValidator.CLI.Helpers;
using CommandLine;

namespace ChecksumValidator.CLI;

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
    
    public static ParsedArgumentsDto ParseArguments(string[] args)
    {
        var parsedArguments = new ParsedArgumentsDto(null, null);
        var parsingResult = Parser?.ParseArguments<Options>(args);

        parsingResult.WithParsed(options =>
        {
            //set parsed values into dto
            parsedArguments.SetFilePath(options.FilePath);
            parsedArguments.SetKnownHash(options.KnownHash);
            //Parse provided string value into AlgoType enum and store into dto
            parsedArguments.SetAlgorithm(GetParsedAlgorithm(options));
        });
        parsingResult.WithNotParsed(errors =>
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
    private static AlgoType GetParsedAlgorithm(Options options)
    {
        if (options.SelectedAlgorithm == null) return AlgoType.Md5;
        try
        {
            return ParseAlgorithm(options.SelectedAlgorithm);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            DisplayHelper.DisplaySupportedAlgorithms();
            throw; // Exit the program with an error code
        }
    }

    private static void HandleGenericError(Error error)
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
    public static AlgoType ParseAlgorithm(string? algorithmString)
    {
        // Attempt to parse the string to an AlgoType enum
        var success = Enum.TryParse(typeof(AlgoType), algorithmString, true, out var algoEnum);

        // If parsing is successful, cast and return the parsed enum value
        if (success)
        {
            // ReSharper disable once NullableWarningSuppressionIsUsed
            //Null check is redundant here thanks to Enum.TryParse method
            return (AlgoType)algoEnum!;
        }


        // If parsing fails, print the supported algorithms and return the default
        throw new InvalidAlgorithmException($"Invalid algorithm specified: {algorithmString ?? "null"}");
    }
}