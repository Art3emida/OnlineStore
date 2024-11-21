namespace OnlineStore.Application.Factory.Users;

using OnlineStore.Application.Command.Users;
using OnlineStore.Domain.Model.Users;

public class UserFactory : IUserFactory
{
    public User CreateFromRegisterCommand(RegisterUserCommand command)
    {
        return new User {
            UserName = command.Email,
            Email = command.Email,
            EmailConfirmed = true,
            Name = command.Name,
            City = command.City,
            State = command.State,
            PostalCode = command.PostalCode,
            StreetAddress = command.StreetAddress,
            PhoneNumber = command.PhoneNumber,
        };
    }
}
