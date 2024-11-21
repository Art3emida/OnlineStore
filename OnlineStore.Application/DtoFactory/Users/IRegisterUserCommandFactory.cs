namespace OnlineStore.Application.DtoFactory.Users;

using OnlineStore.Application.Command.Users;
using OnlineStore.Application.DtoFactory.Common;
using OnlineStore.Domain.Model.Users;

public interface IRegisterUserCommandFactory : IDtoFactory
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
    );
}
