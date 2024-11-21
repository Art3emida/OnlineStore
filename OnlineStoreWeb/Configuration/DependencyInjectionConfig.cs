namespace OnlineStoreWeb.Configuration;

using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Application;
using OnlineStore.Application.Api.Repositories.Checkout;
using OnlineStore.Application.Api.Repositories.Products;
using OnlineStore.Application.Api.Services.Common;
using OnlineStore.Application.DtoFactory.Checkout;
using OnlineStore.Application.DtoFactory.Common;
using OnlineStore.Application.DtoFactory.Products;
using OnlineStore.Application.DtoFactory.Users;
using OnlineStore.Application.Factory.Checkout;
using OnlineStore.Application.Factory.Products;
using OnlineStore.Application.Factory.Users;
using OnlineStore.Application.Services.Checkout;
using OnlineStore.Application.Services.Products;
using OnlineStore.Application.Services.Users;
using OnlineStore.Domain.Model.Users;
using OnlineStore.Infrastructure.Persistence.Context;
using OnlineStore.Infrastructure.Persistence.Initializers;
using OnlineStore.Infrastructure.Repositories.Checkout;
using OnlineStore.Infrastructure.Repositories.Products;
using OnlineStore.Infrastructure.Services.Common;

public class DependencyInjectionConfig
{
    public static void RegisterServices(IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
        services.AddScoped<IProductQueryService, ProductQueryService>();
        services.AddScoped<IProductCategoryQueryRepository, ProductCategoryQueryRepository>();
        services.AddScoped<IProductCategoryQueryService, ProductCategoryQueryService>();
        services.AddScoped<IUserRoleQueryService, UserRoleQueryService>();
        services.AddScoped<IUserRoleCommandService, UserRoleCommandService>();
        services.AddScoped<IUserRoleFactory, UserRoleFactory>();
        services.AddScoped<IUserFactory, UserFactory>();
        services.AddScoped<ITransactionOperationService, TransactionOperationService>();
        services.AddScoped<IProductQueryFactory, ProductQueryFactory>();
        services.AddScoped<IProductCategoryQueryFactory, ProductCategoryQueryFactory>();
        services.AddScoped<ICreateProductCommandFactory, CreateProductCommandFactory>();
        services.AddScoped<ICreateProductCategoryCommandFactory, CreateProductCategoryCommandFactory>();
        services.AddScoped<ICreateCartItemCommandFactory, CreateCartItemCommandFactory>();
        services.AddScoped<IProductCategoryFactory, ProductCategoryFactory>();
        services.AddScoped<IProductFactory, ProductFactory>();
        services.AddScoped<IProductPhotoFactory, ProductPhotoFactory>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IUserCommandService, UserCommandService>();
        services.AddScoped<IProductCommandService, ProductCommandService>();
        services.AddScoped<IProductCategoryCommandService, ProductCategoryCommandService>();
        services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
        services.AddScoped<IProductCategoryCommandRepository, ProductCategoryCommandRepository>();
        services.AddScoped<IPaginationControlFactory, PaginationControlFactory>();
        services.AddScoped<IPaginationFactory, PaginationFactory>();
        services.AddScoped<ICartFactory, CartFactory>();
        services.AddScoped<ICartQueryRepository, CartQueryRepository>();
        services.AddScoped<ICartCommandRepository, CartCommandRepository>();
        services.AddScoped<ICartCommandService, CartCommandService>();
        services.AddScoped<ICartQueryService, CartQueryService>();
        services.AddScoped<ICartItemFactory, CartItemFactory>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IUpdateProductCategoryCommandFactory, UpdateProductCategoryCommandFactory>();
        services.AddScoped<IUpdateProductCommandFactory, UpdateProductCommandFactory>();
        services.AddScoped<IRegisterUserCommandFactory, RegisterUserCommandFactory>();
        services.AddScoped<ILoginUserCommandFactory, LoginUserCommandFactory>();
        services.AddScoped<IUserQueryService, UserQueryService>();
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped(typeof(IValidationService<>), typeof(ValidationService<>));
        services.AddScoped(typeof(IListStructureFactory<>), typeof(ListStructureFactory<>));
        services.AddDbContext<MasterDbContext>(
            options => options.UseSqlServer(
                configuration.GetConnectionString("MasterConnection")
            )
        );
        services.AddIdentity<User, UserRole>()
            .AddEntityFrameworkStores<MasterDbContext>()
            .AddDefaultTokenProviders()
            ;
        services.Configure<IdentityOptions>(
            options => {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }
        );
        services.AddControllersWithViews(options => {
            options.Filters.Add<ControllerExceptionFilter>();
        }).AddFluentValidation(fv => {
            fv.RegisterValidatorsFromAssemblyContaining<Program>();
            fv.RegisterValidatorsFromAssemblyContaining<ApplicationRoot>();
        });
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(3);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        services.AddDistributedMemoryCache();
    }
}
