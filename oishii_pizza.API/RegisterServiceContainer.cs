using oishii_pizza.Domain.Features.ProductService;
using oishii_pizza.Domain.Features.TypeOfProductService;
using oishii_pizza.Domain.Features.UserService;
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
            return services;
        }
    }
}
