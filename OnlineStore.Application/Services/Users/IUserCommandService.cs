namespace OnlineStore.Application.Services.Users;

using OnlineStore.Application.Command.Users;

public interface IUserCommandService
{
    Task RegisterAsync(RegisterUserCommand command);
    Task LoginAsync(LoginUserCommand command);
    Task LogoutAsync();
}
