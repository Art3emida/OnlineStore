namespace OnlineStore.Application.Exceptions.Users;

using OnlineStore.Application.Exceptions.Common;

public class UserLoginException : ValidationException
{
    public UserLoginException(string message) : base(message)
    {
    }
}
