using oishii_pizza.Domain.Features.UserService;
using oishii_pizza.Infrastructure.Repositories.UserRepository;

namespace oishii_pizza.API
{
    public static class RegisterServiceContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
