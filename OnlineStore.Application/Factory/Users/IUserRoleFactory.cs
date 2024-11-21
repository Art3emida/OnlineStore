namespace OnlineStore.Application.Factory.Users;

using OnlineStore.Application.Factory.Common;
using OnlineStore.Domain.Model.Users;

public interface IUserRoleFactory : IFactory
{
    UserRole Create(
        RoleType roleType
    );
}
