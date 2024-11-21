namespace OnlineStore.Application.Factory.Users;

using OnlineStore.Domain.Model.Users;

public class UserRoleFactory : IUserRoleFactory
{
    public UserRole Create(
        RoleType roleType
    ) {
        return new UserRole {
            Name = roleType.ToString(),
        };
    }
}
