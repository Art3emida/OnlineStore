namespace OnlineStore.Application.Exceptions.Common;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}
