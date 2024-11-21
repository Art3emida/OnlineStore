namespace OnlineStore.Application.Factory.Users;

using OnlineStore.Application.Command.Users;
using OnlineStore.Application.Factory.Common;
using OnlineStore.Domain.Model.Users;

public interface IUserFactory : IFactory
{
    User CreateFromRegisterCommand(RegisterUserCommand dto);
}
