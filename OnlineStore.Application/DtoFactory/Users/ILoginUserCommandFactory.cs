namespace OnlineStore.Application.DtoFactory.Users;

using OnlineStore.Application.Command.Users;
using OnlineStore.Application.DtoFactory.Common;

public interface ILoginUserCommandFactory : IDtoFactory
{
    public LoginUserCommand Create(
        string email,
        string password,
        bool rememberMe
    );
}
