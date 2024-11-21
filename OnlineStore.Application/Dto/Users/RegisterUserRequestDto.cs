namespace OnlineStore.Application.Dto.Users;

using OnlineStore.Application.Dto.Common;

public record RegisterUserRequestDto(
    string Email,
    string Name,
    string Password,
    string ConfirmPassword,
    string? PhoneNumber,
    string? StreetAddress,
    string? City,
    string? State,
    string? PostalCode
): IDto;
