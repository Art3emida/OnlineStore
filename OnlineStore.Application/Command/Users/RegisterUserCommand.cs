namespace OnlineStore.Application.Command.Users;

using OnlineStore.Application.Dto.Common;
using OnlineStore.Domain.Model.Users;

public record RegisterUserCommand(
    string Email,
    string Password,
    string Name,
    string? PhoneNumber,
    string? StreetAddress,
    string? City,
    string? State,
    string? PostalCode,
    RoleType? RoleType,
    bool AutoSignIn
): IDto;
