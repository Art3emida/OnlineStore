namespace OnlineStore.Application.Command.Users;

using OnlineStore.Application.Dto.Common;

public record LoginUserCommand(
    string Email,
    string Password,
    bool RememberMe
): IDto;
