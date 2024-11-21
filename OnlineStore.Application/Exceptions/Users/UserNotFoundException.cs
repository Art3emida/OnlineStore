namespace OnlineStore.Application.Exceptions.Users;

using OnlineStore.Application.Exceptions.Common;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string message) : base(message)
    {
    }
}
