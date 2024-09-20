using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Repositories.Implementations;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Implementations;
using ePizzaHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ePizzaHub.Services
{
    public static class ConfigureServices
    {
        public static void RegisteredServices(IServiceCollection services, IConfiguration config)
        {
            //DB
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DbConnection"));
            });

            //Reposiotories
            services.AddScoped<IRepository<Item>, Repository<Item>>();
            services.AddScoped<IRepository<CartItem>, Repository<CartItem>>();
            services.AddScoped<IRepository<PaymentDetail>, Repository<PaymentDetail>>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            //Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPaymentSevice, PaymentSevice>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
