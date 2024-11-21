namespace OnlineStoreWeb.Areas.Shop.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Services.Checkout;
using OnlineStore.Domain.Model.Checkout;

[Area("shop")]
public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [Route("cart")]
    public async Task<IActionResult> Index()
    {
        Cart cart = await _cartService.GetCartAsync(GetLoggedUserId());

        return View(cart);
    }

    [HttpPost]
    [Route("cart/add")]
    public async Task<IActionResult> Add(int productId, int quantity)
    {
        await _cartService.AddToCartAsync(productId, quantity, GetLoggedUserId());

        return Ok();
    }

    [HttpPost]
    [Route("cart/remove")]
    public async Task<IActionResult> Remove(int productId)
    {
        int? userId = GetLoggedUserId();
        await _cartService.RemoveFromCartAsync(productId, userId);
        Cart cart = await _cartService.GetCartAsync(userId);

        return PartialView("_Cart", cart);
    }

    [HttpPost]
    [Route("cart/update")]
    public async Task<IActionResult> UpdateQuantity(int productId, int quantity)
    {
        int? userId = GetLoggedUserId();
        await _cartService.UpdateQuantityAsync(productId, quantity, userId);
        Cart cart = await _cartService.GetCartAsync(userId);

        return PartialView("_Cart", cart);
    }

    private int? GetLoggedUserId()
    {
        string? userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return userId != null ? int.Parse(userId) : null;
    }
}
