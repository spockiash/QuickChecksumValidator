namespace ChecksumValidator.CLI;

public class InvalidAlgorithmException : Exception
{
    public InvalidAlgorithmException(string message)
        : base(message)
    {
    }
}