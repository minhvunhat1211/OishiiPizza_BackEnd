using oishii_pizza.Domain.Common.FileStorage;
using oishii_pizza.Domain.Features.OrderService;
using oishii_pizza.Domain.Features.ProductService;
using oishii_pizza.Domain.Features.TypeOfProductService;
using oishii_pizza.Domain.Features.UserService;
using oishii_pizza.Infrastructure.Repositories.OrderRepository;
using oishii_pizza.Infrastructure.Repositories.ProductRepository;
using oishii_pizza.Infrastructure.Repositories.TypeOfProductRepository;
using oishii_pizza.Infrastructure.Repositories.UserRepository;

namespace oishii_pizza.API
{
    public static class RegisterServiceContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITypeOfProductService, TypeOfProductService>();
            services.AddTransient<ITypeOfProductRepository, TypeOfProductRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IFileStorageService, FileStorageService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
