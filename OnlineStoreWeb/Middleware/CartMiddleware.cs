namespace OnlineStoreWeb.Middleware;

using System.Security.Claims;
using OnlineStore.Application.Services.Checkout;
using OnlineStore.Domain.Model.Checkout;

public class CartMiddleware
{
    private readonly RequestDelegate _next;

    public CartMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            ICartService cartService = scope.ServiceProvider.GetRequiredService<ICartService>();
            Cart cart = await cartService.GetCartAsync(GetLoggedUserId(httpContext));
            httpContext.Items["Cart"] = cart;
            httpContext.Items["CartItemCount"] = cart.Items.Count;
        }

        await _next(httpContext);
    }

    private int? GetLoggedUserId(HttpContext httpContext)
    {
        string? userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return userId != null ? int.Parse(userId) : null;
    }
}
