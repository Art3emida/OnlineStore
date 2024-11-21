namespace OnlineStoreWeb.Areas.Identity.Controllers;

using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Command.Users;
using OnlineStore.Application.Dto.Users;
using OnlineStore.Application.DtoFactory.Users;
using OnlineStore.Application.Exceptions.Common;
using OnlineStore.Application.Services.Users;

[Area("identity")]
public class AccountController : Controller
{
    [Route("account/register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("account/register")]
    public async Task<IActionResult> Register(
        RegisterUserRequestDto requestDto,
        [FromServices] IRegisterUserCommandFactory commandFactory,
        [FromServices] IUserCommandService commandService
    ) {
        if (ModelState.IsValid)
        {
            RegisterUserCommand command = commandFactory.Create(
                email: requestDto.Email,
                name: requestDto.Name,
                city: requestDto.City,
                state: requestDto.State,
                postalCode: requestDto.PostalCode,
                streetAddress: requestDto.StreetAddress,
                phoneNumber: requestDto.PhoneNumber,
                password: requestDto.Password,
                autoSignIn: true
            );

            try
            {
                await commandService.RegisterAsync(command);
                return RedirectToMainPage();
            }
            catch (ValidationException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
        }

        return View(requestDto);
    }

    [Route("account/login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("account/login")]
    public async Task<IActionResult> Login(
        LoginUserRequestDto requestDto,
        [FromServices] ILoginUserCommandFactory commandFactory,
        [FromServices] IUserCommandService commandService
    ) {
        if (ModelState.IsValid)
        {
            LoginUserCommand command = commandFactory.Create(
                email: requestDto.Email,
                password: requestDto.Password,
                rememberMe: requestDto.RememberMe
            );

            try
            {
                await commandService.LoginAsync(command);
                return RedirectToMainPage();
            }
            catch (ValidationException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
        }

        return View(requestDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("account/logout")]
    public async Task<IActionResult> Logout(
        [FromServices] IUserCommandService commandService
    ) {
        await commandService.LogoutAsync();

        return RedirectToMainPage();
    }

    private IActionResult RedirectToMainPage()
    {
        return RedirectToAction("index", "Site", new { area = "Common" });
    }
}
