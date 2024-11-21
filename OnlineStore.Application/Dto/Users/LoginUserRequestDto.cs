namespace OnlineStore.Application.Dto.Users;

using OnlineStore.Application.Dto.Common;

public record LoginUserRequestDto(
    string Email,
    string Password,
    bool RememberMe
): IDto;
