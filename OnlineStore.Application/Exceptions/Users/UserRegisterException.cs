namespace OnlineStore.Application.Exceptions.Users;

using OnlineStore.Application.Exceptions.Common;

public class UserRegisterException : ValidationException
{
    public UserRegisterException(string message) : base(message)
    {
    }
}
