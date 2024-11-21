namespace OnlineStore.Infrastructure.Persistence.Initializers;

using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.DtoFactory.Products;
using OnlineStore.Application.DtoFactory.Users;
using OnlineStore.Application.Services.Products;
using OnlineStore.Application.Services.Users;
using OnlineStore.Domain.Model.Users;
using OnlineStore.Infrastructure.Persistence.Context;

public class DbInitializer : IDbInitializer
{
    private readonly MasterDbContext _dbContext;
    private readonly IRegisterUserCommandFactory _userCommandFactory;
    private readonly IUserCommandService _userCommandService;
    private readonly IUserRoleCommandService _userRoleCommandService;
    private readonly IUserRoleQueryService _userRoleQueryService;
    private readonly ICreateProductCommandFactory _productCommandFactory;
    private readonly IProductCommandService _productCommandService;
    private readonly ICreateProductCategoryCommandFactory _productCategoryCommandFactory;
    private readonly IProductCategoryCommandService _productCategoryCommandService;

    public DbInitializer(
        MasterDbContext dbContext,
        IRegisterUserCommandFactory userCommandFactory,
        IUserCommandService userCommandService,
        IUserRoleCommandService userRoleCommandService,
        IUserRoleQueryService userRoleQueryService,
        ICreateProductCommandFactory productCommandFactory,
        IProductCommandService productCommandService,
        ICreateProductCategoryCommandFactory productCategoryCommandFactory,
        IProductCategoryCommandService productCategoryCommandService
    ) {
        _dbContext = dbContext;
        _userCommandFactory = userCommandFactory;
        _userCommandService = userCommandService;
        _userRoleCommandService = userRoleCommandService;
        _userRoleQueryService = userRoleQueryService;
        _productCommandFactory = productCommandFactory;
        _productCommandService = productCommandService;
        _productCategoryCommandFactory = productCategoryCommandFactory;
        _productCategoryCommandService = productCategoryCommandService;
    }

    public async Task InitializeAsync()
    {
        if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await _dbContext.Database.MigrateAsync();

            if (!await _dbContext.ProductCategories.AnyAsync())
            {
                await SeedProductCategories();
            }

            if (!await _dbContext.Products.AnyAsync())
            {
                await SeedProducts();
            }
            
            if (!await _dbContext.UserRoles.AnyAsync())
            {
                await SeedUserRoles();
            }
            
            if (!await _dbContext.Users.AnyAsync())
            {
                await SeedUsers();
            }
        }
    }
    
    private async Task SeedUsers()
    {
        var users = new[]
        {
            new 
            {
                RoleType = RoleType.Admin,
                Email = "admin@mail.com",
                Password = "Password_1234",
                Name = "Admin",
                City = "Chicago",
                State = "IL",
                PostalCode = "77777",
                StreetAddress = "Admin st.",
                PhoneNumber = "380930000001"
            },
            new 
            {
                RoleType = RoleType.Customer,
                Email = "customer@mail.com",
                Password = "Password_1234",
                Name = "Test Customer",
                City = "New York",
                State = "FD",
                PostalCode = "88000",
                StreetAddress = "Customer st.",
                PhoneNumber = "380930000002"
            }
        };

        foreach (var user in users)
        {
            var command = _userCommandFactory.Create(
                roleType: user.RoleType,
                email: user.Email,
                password: user.Password,
                name: user.Name,
                city: user.City,
                state: user.State,
                postalCode: user.PostalCode,
                streetAddress: user.StreetAddress,
                phoneNumber: user.PhoneNumber
            );
            await _userCommandService.RegisterAsync(command);
        }
    }
    
    private async Task SeedUserRoles()
    {
        await _userRoleCommandService.CreateAsync(RoleType.Admin);
        await _userRoleCommandService.CreateAsync(RoleType.Customer);
    }

    private async Task SeedProductCategories()
    {
        var categories = new[]
        {
            new { Name = "Electronics", Description = "Electronic items and gadgets" },
            new { Name = "Books", Description = "Various genres of books" },
            new { Name = "Clothing", Description = "Apparel and accessories" }
        };

        foreach (var category in categories)
        {
            var command = _productCategoryCommandFactory.Create(
                name: category.Name,
                description: category.Description
            );
            await _productCategoryCommandService.CreateAsync(command);
        }
    }

    private async Task SeedProducts()
    {
        var categories = await _dbContext.ProductCategories
            .Where(c => c.Name == "Electronics" || c.Name == "Books")
            .ToListAsync();

        var products = new[]
        {
            new
            {
                CategoryName = "Electronics",
                Products = new[]
                {
                    new { Name = "Smartphone", Description = "Latest model smartphone with high-end features", Price = 999.99m },
                    new { Name = "Laptop", Description = "Powerful laptop for work and gaming", Price = 1499.99m },
                    new { Name = "Tablet", Description = "Portable tablet with touchscreen", Price = 499.99m },
                    new { Name = "Headphones", Description = "Noise-cancelling over-ear headphones", Price = 199.99m },
                    new { Name = "Smartwatch", Description = "Wearable device to track your health", Price = 249.99m },
                    new { Name = "Bluetooth Speaker", Description = "Portable wireless speaker", Price = 89.99m },
                    new { Name = "Camera", Description = "Digital camera with 4K video recording", Price = 799.99m },
                    new { Name = "VR Headset", Description = "Virtual reality headset for immersive experiences", Price = 299.99m },
                    new { Name = "External Hard Drive", Description = "Portable storage device with 1TB capacity", Price = 79.99m },
                    new { Name = "Gaming Console", Description = "Next-gen gaming console with 4K support", Price = 499.99m },
                    new { Name = "Power Bank", Description = "Portable charger for your devices", Price = 39.99m },
                    new { Name = "Smart Home Hub", Description = "Control your smart home devices from one place", Price = 129.99m },
                    new { Name = "Electric Toothbrush", Description = "Advanced toothbrush with multiple cleaning modes", Price = 89.99m },
                    new { Name = "Drone", Description = "Camera drone with HD video capability", Price = 399.99m },
                    new { Name = "Projector", Description = "Portable projector for home entertainment", Price = 299.99m }
                }
            },
            new
            {
                CategoryName = "Books",
                Products = new[]
                {
                    new { Name = "Programming in C#", Description = "Comprehensive guide to C# programming for beginners and experts", Price = 49.99m },
                    new { Name = "Science Fiction Novel", Description = "A thrilling adventure in a futuristic universe", Price = 24.99m },
                    new { Name = "Cookbook", Description = "Recipes from around the world", Price = 34.99m },
                    new { Name = "The Great Gatsby", Description = "Classic novel by F. Scott Fitzgerald", Price = 15.99m },
                    new { Name = "To Kill a Mockingbird", Description = "Pulitzer Prize-winning novel by Harper Lee", Price = 18.99m },
                    new { Name = "1984", Description = "Dystopian novel by George Orwell", Price = 16.99m },
                    new { Name = "Pride and Prejudice", Description = "Romantic novel by Jane Austen", Price = 12.99m },
                    new { Name = "Moby Dick", Description = "Epic novel by Herman Melville", Price = 19.99m },
                    new { Name = "War and Peace", Description = "Historical novel by Leo Tolstoy", Price = 22.99m },
                    new { Name = "The Catcher in the Rye", Description = "Novel by J.D. Salinger", Price = 14.99m },
                    new { Name = "The Hobbit", Description = "Fantasy novel by J.R.R. Tolkien", Price = 20.99m },
                    new { Name = "Crime and Punishment", Description = "Psychological novel by Fyodor Dostoevsky", Price = 17.99m },
                    new { Name = "Brave New World", Description = "Dystopian novel by Aldous Huxley", Price = 16.49m },
                    new { Name = "The Alchemist", Description = "Philosophical novel by Paulo Coelho", Price = 14.49m },
                    new { Name = "The Lord of the Rings", Description = "Epic fantasy novel by J.R.R. Tolkien", Price = 29.99m }
                }
            }
        };

        foreach (var categoryData in products)
        {
            var category = categories.FirstOrDefault(c => c.Name == categoryData.CategoryName);

            if (category != null)
            {
                foreach (var product in categoryData.Products)
                {
                    var command = _productCommandFactory.Create(
                        name: product.Name,
                        description: product.Description,
                        price: product.Price,
                        categoryId: category.Id
                    );
                    await _productCommandService.CreateAsync(command);
                }
            }
        }
    }
}
