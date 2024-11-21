namespace OnlineStore.Infrastructure.Repositories.Checkout;

using OnlineStore.Application.Api.Repositories.Checkout;
using OnlineStore.Domain.Model.Checkout;
using OnlineStore.Infrastructure.Persistence.Context;
using OnlineStore.Infrastructure.Repositories.Common;

public class CartCommandRepository : CommandRepository<Cart, string>, ICartCommandRepository
{
    public CartCommandRepository(MasterDbContext dbContext) : base(dbContext) {}

    public new async Task CreateAsync(Cart cart)
    {
        await base.CreateAsync(cart);
    }

    public new async Task UpdateAsync(Cart cart)
    {
        await base.UpdateAsync(cart);
    }

    public new async Task DeleteAsync(Cart cart)
    {
        await base.DeleteAsync(cart);
    }

    public new async Task DeleteByIdAsync(string id)
    {
        await base.DeleteByIdAsync(id);
    }
}
