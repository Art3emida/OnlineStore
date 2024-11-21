namespace OnlineStore.Application.Services.Users;

using Microsoft.AspNetCore.Identity;
using OnlineStore.Application.Exceptions.Users;
using OnlineStore.Application.Factory.Users;
using OnlineStore.Domain.Model.Users;

public class UserRoleCommandService : IUserRoleCommandService
{
    private readonly IUserRoleFactory _roleFactory;
    private readonly RoleManager<UserRole> _roleManager;

    public UserRoleCommandService(
        IUserRoleFactory roleFactory,
        RoleManager<UserRole> roleManager
    ) {
        _roleFactory = roleFactory;
        _roleManager = roleManager;
    }

    public async Task CreateAsync(RoleType roleType)
    {
        UserRole role = _roleFactory.Create(roleType);
        IdentityResult result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            throw new UserRoleCreationException(string.Format(
                "Failed to create role '{0}': {1}",
                roleType.ToString(),
                string.Join("; ", result.Errors.Select(e => e.Description))
            ));
        }
    }
}
