namespace OnlineStore.Application.Services.Users;

using Microsoft.AspNetCore.Identity;
using OnlineStore.Domain.Model.Users;

public class UserRoleQueryService : IUserRoleQueryService
{
    private readonly RoleManager<UserRole> _roleManager;

    public UserRoleQueryService(
        RoleManager<UserRole> roleManager
    ) {
        _roleManager = roleManager;
    }

    public async Task<bool> ExistsAsync(RoleType roleType)
    {
        return await _roleManager.RoleExistsAsync(roleType.ToString());
    }
}
