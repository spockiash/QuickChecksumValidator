using System.Text;
using ChecksumValidator.CLI.Dto;
using ChecksumValidator.CLI.Enums;
using CommandLine;
using CommandLine.Text;

namespace ChecksumValidator.CLI.Helpers;

public static class DisplayHelper
{
    public static void DisplayError(string error)
    {
        Console.WriteLine(DisplayResources.ErrorMessage, error);
    }

    public static void DisplaySupportedAlgorithms()
    {
        Console.WriteLine(DisplayResources.DisplaySupportedAlgorithms);
        var sb = new StringBuilder();
        var enumType = typeof(AlgoType);
        var names = Enum.GetNames(enumType);
        var counter = 0;
        foreach (var name in names)
        {
            if (counter == names.Length - 1)
            {
                sb.Append($"and {name}");
            }
            else
            {
                sb.Append($"{name}, ");
            }
            counter++;
        }
        Console.WriteLine(sb.ToString());
    }
    public static void DisplayUnknownOptionError(string? token)
    {
        Console.WriteLine(DisplayResources.ErrorUnknownOption, token ?? string.Empty);
        DisplayUsage();
    }
    private static void DisplayUsage()
    {
        Console.WriteLine(DisplayResources.HelpUsage);
    }
    public static void DisplayHelp(ParserResult<Options> result)
    {
        var helpText = HelpText.AutoBuild(result, help =>
        {
            help.Heading = DisplayResources.HelpHeading;
            help.Copyright = DisplayResources.HelpCopyright;
            help.AdditionalNewLineAfterOption = true;          // Add some formatting
            help.AddPreOptionsLine(DisplayResources.HelpUsage);
            return HelpText.DefaultParsingErrorsHandler(result, help);
        }, e => e);
        Console.WriteLine(helpText);
    }

    public static void DisplayResult(ValidationResultDto result)
    {
        if (result.ValidationSuccess)
        {
            Console.WriteLine(DisplayResources.ValidationSuccessMessage);
        }
        else
        {
            Console.WriteLine(DisplayResources.ValidationFailMessage);
        }
        Console.WriteLine();
        Console.WriteLine(DisplayResources.FileChekcedMessage, result.PathToFile);
        Console.WriteLine(DisplayResources.SelectedAlgorithmMessage, result.UsedAlgorithm);
        Console.WriteLine(DisplayResources.ComputedChecksumMessage, result.CalculatedHash);
        Console.WriteLine(DisplayResources.ProvidedChecksumMessage, result.KnownHash);
    }
}