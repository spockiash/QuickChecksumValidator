namespace CLI_Tool;

public class InvalidAlgorithmException : Exception
{
    public InvalidAlgorithmException(string message)
        : base(message)
    {
    }
}