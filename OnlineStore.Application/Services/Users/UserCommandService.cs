namespace OnlineStore.Application.Services.Users;

using Microsoft.AspNetCore.Identity;
using OnlineStore.Application.Api.Services.Common;
using OnlineStore.Application.ConstantBag.Users;
using OnlineStore.Application.Command.Users;
using OnlineStore.Application.Exceptions.Users;
using OnlineStore.Application.Factory.Users;
using OnlineStore.Application.Services.Common;
using OnlineStore.Domain.Model.Users;

public class UserCommandService : CommandService, IUserCommandService
{
    private readonly ITransactionOperationService _transactionOperationService;
    private readonly IUserFactory _userFactory;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserCommandService(
        IValidationService validationService,
        ITransactionOperationService transactionOperationService,
        IUserFactory userFactory,
        UserManager<User> userManager,
        SignInManager<User> signInManager
    ) : base(
        validationService
    ) {
        _transactionOperationService = transactionOperationService;
        _userFactory = userFactory;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task RegisterAsync(RegisterUserCommand command)
    {
        await ValidateCommandAsync(command);
        User user = _userFactory.CreateFromRegisterCommand(command);

        await _transactionOperationService.ExecuteAsTransactionAsync(async () => {
            IdentityResult result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                throw new UserRegisterException(result.Errors.First().Description);
            }

            if (command.RoleType != null)
            {
                await _userManager.AddToRoleAsync(user, command.RoleType.ToString());
            }

            if (command.AutoSignIn)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
        });
    }

    public async Task LoginAsync(LoginUserCommand command)
    {
        await ValidateCommandAsync(command);
        SignInResult result = await _signInManager.PasswordSignInAsync(
            command.Email,
            command.Password,
            command.RememberMe,
            lockoutOnFailure: true
        );

        if (!result.Succeeded)
        {
            throw new UserLoginException(result.IsLockedOut
                ? UserExceptionBag.InvalidLoginAttemptUserLocked
                : UserExceptionBag.InvalidLoginAttempt
            );
        }
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
