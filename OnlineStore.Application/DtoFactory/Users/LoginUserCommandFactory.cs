namespace OnlineStore.Application.DtoFactory.Users;

using OnlineStore.Application.Command.Users;

public class LoginUserCommandFactory : ILoginUserCommandFactory
{
    public LoginUserCommand Create(
        string email,
        string password,
        bool rememberMe
    ) {
        return new LoginUserCommand(
            Email: email,
            Password: password,
            RememberMe: rememberMe
        );
    }
}
