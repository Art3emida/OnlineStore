namespace OnlineStore.Application.Exceptions.Common;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}
