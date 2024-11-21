namespace OnlineStore.Application.Services.Checkout;

using Microsoft.AspNetCore.Http;
using OnlineStore.Application.Command.Checkout;
using OnlineStore.Application.DtoFactory.Checkout;
using OnlineStore.Application.Exceptions.Checkout;
using OnlineStore.Application.Factory.Checkout;
using OnlineStore.Application.Services.Products;
using OnlineStore.Domain.Model.Checkout;
using OnlineStore.Domain.Model.Products;

public class CartService : ICartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICartQueryService _cartQueryService;
    private readonly ICartCommandService _cartCommandService;
    private readonly ICartItemFactory _cartItemFactory;
    private readonly IProductQueryService _productQueryService;
    private readonly ICreateCartItemCommandFactory _createCartItemCommandFactory;

    public CartService(
        IHttpContextAccessor httpContextAccessor,
        ICartQueryService cartQueryService,
        ICartCommandService cartCommandService,
        ICartItemFactory cartItemFactory,
        IProductQueryService productQueryService,
        ICreateCartItemCommandFactory createCartItemCommandFactory
    ) {
        _httpContextAccessor = httpContextAccessor;
        _cartQueryService = cartQueryService;
        _cartCommandService = cartCommandService;
        _cartItemFactory = cartItemFactory;
        _productQueryService = productQueryService;
        _createCartItemCommandFactory = createCartItemCommandFactory;
    }

    public async Task AddToCartAsync(int productId, int quantity, int? userId = null)
    {
        Cart cart = await GetCartAsync(userId);
        CartItem? cartItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

        if (cartItem != null)
        {
            cartItem.Quantity += quantity;
        }
        else
        {
            Product product = await _productQueryService.GetByIdAsync(productId);
            CreateCartItemCommand command = _createCartItemCommandFactory.Create(
                cartId: cart.Id,
                productId: productId,
                name: product.Name,
                quantity: quantity,
                price: product.Price
            );
            CartItem item = _cartItemFactory.CreateFromCommand(command);
            cart.Items.Add(item);
        }
        
        await _cartCommandService.UpdateAsync(cart);
    }

    public async Task RemoveFromCartAsync(int productId, int? userId = null)
    {
        Cart cart = await GetCartAsync(userId);
        CartItem? cartItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

        if (cartItem != null)
        {
            cart.Items.Remove(cartItem);
            await _cartCommandService.UpdateAsync(cart);
        }
    }

    public async Task UpdateQuantityAsync(int productId, int quantity, int? userId = null)
    {
        Cart cart = await GetCartAsync(userId);
        CartItem? cartItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

        if (cartItem != null)
        {
            cartItem.Quantity = quantity;
            await _cartCommandService.UpdateAsync(cart);
        }
    }

    public async Task<int> GetCartItemCountAsync(int? userId = null)
    {
        Cart cart = await GetCartAsync(userId);
        return cart.Items.Count;
    }

    public async Task<Cart> GetCartAsync(int? userId = null)
    {
        if (userId != null)
        {
            Cart cart;
            
            try
            {
                cart = await _cartQueryService.GetByUserIdAsync(userId.Value);
            }
            catch (CartNotFoundException)
            {
                await _cartCommandService.CreateAsync(userId.Value);
                cart = await _cartQueryService.GetByUserIdAsync(userId.Value);
            }

            return cart;
        }

        string? cartIdInSession = _httpContextAccessor.HttpContext.Session.GetString("CartId");

        if (!string.IsNullOrEmpty(cartIdInSession))
        {
            try
            {
                Cart cart = await _cartQueryService.GetByIdAsync(cartIdInSession);
                return cart;
            }
            catch (CartNotFoundException)
            {
            }
        }

        string cartId = Guid.NewGuid().ToString();
        await _cartCommandService.CreateAsync(0, cartId);
        _httpContextAccessor.HttpContext.Session.SetString("CartId", cartId);

        return await _cartQueryService.GetByIdAsync(cartId);
    }
}
