namespace Weather.Common.Exceptions;

public class MissingSettingsException : Exception
{
    public MissingSettingsException() : 
        base("Settings have not been added to the service collection!")
    {
        
    }
}