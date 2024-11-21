namespace OnlineStore.Application.DtoFactory.Users;

using OnlineStore.Application.Command.Users;
using OnlineStore.Domain.Model.Users;

public class RegisterUserCommandFactory : IRegisterUserCommandFactory
{
    public RegisterUserCommand Create(
        string email,
        string password,
        string name,
        string? phoneNumber = null,
        string? streetAddress = null,
        string? city = null,
        string? state = null,
        string? postalCode = null,
        RoleType? roleType = null,
        bool autoSignIn = false
    ) {
        return new RegisterUserCommand(
            Email: email,
            Password: password,
            Name: name,
            PhoneNumber: phoneNumber,
            StreetAddress: streetAddress,
            City: city,
            State: state,
            PostalCode: postalCode,
            RoleType: roleType,
            AutoSignIn: autoSignIn
        );
    }
}
