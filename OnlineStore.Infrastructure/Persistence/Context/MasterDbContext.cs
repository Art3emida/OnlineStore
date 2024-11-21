namespace OnlineStore.Infrastructure.Persistence.Context;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Model.Checkout;
using OnlineStore.Domain.Model.Products;
using OnlineStore.Domain.Model.Users;

public class MasterDbContext : IdentityDbContext<User, UserRole, int>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductPhoto> ProductPhotos { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
