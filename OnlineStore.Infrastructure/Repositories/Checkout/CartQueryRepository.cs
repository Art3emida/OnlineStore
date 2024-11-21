namespace OnlineStore.Infrastructure.Repositories.Checkout;

using System.Linq.Expressions;
using OnlineStore.Application.Api.Repositories.Checkout;
using OnlineStore.Application.DtoFactory.Common;
using OnlineStore.Domain.Model.Checkout;
using OnlineStore.Infrastructure.Persistence.Context;
using OnlineStore.Infrastructure.Repositories.Common;

public class CartQueryRepository : QueryRepository<Cart, string>, ICartQueryRepository
{
    public CartQueryRepository(
        MasterDbContext dbContext,
        IListStructureFactory<Cart> listStructureFactory
    ) : base(
        dbContext,
        listStructureFactory
    ) {}

    public async Task<Cart?> GetByIdAsync(string id)
    {
        var includes = new List<Expression<Func<Cart, object>>>();
        includes.Add(c => c.Items);

        return await base.GetByIdAsync(
            id,
            includes: includes
        );
    }

    public async Task<Cart?> GetByUserIdAsync(int userId)
    {
        var includes = new List<Expression<Func<Cart, object>>>();
        includes.Add(c => c.Items);

        return await FindOneAsync(
            predicate: c => c.UserId == userId,
            includes: includes
        );
    }
}
