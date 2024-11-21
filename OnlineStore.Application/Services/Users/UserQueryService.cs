namespace OnlineStore.Application.Services.Users;

using Microsoft.AspNetCore.Identity;
using OnlineStore.Application.ConstantBag.Users;
using OnlineStore.Application.Exceptions.Users;
using OnlineStore.Domain.Model.Users;

public class UserQueryService : IUserQueryService
{
    private readonly UserManager<User> _userManager;

    public UserQueryService(
        UserManager<User> userManager
    ) {
        _userManager = userManager;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        User? user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            throw new UserNotFoundException(string.Format(
                UserExceptionBag.UserNotFound,
                id
            ));
        }

        return user;
    }
}
