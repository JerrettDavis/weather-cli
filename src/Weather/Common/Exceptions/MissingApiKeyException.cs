namespace Weather.Common.Exceptions;

public class MissingApiKeyException : Exception
{
    public MissingApiKeyException() : base("No API Key was provided") {}
}