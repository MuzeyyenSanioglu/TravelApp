using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelApp.Domain.Repositories;
using TravelApp.Domain.Repositories.Base;
using TravelApp.Infrastructure.Data;
using TravelApp.Infrastructure.Repositories;
using TravelApp.Infrastructure.Repositories.Base;

namespace TravelApp.Infrastructure
{
    public static class DependencyInjeciton
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<OrderContext>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"),
            //    ServiceLifetime.Singleton,
            //    ServiceLifetime.Singleton);

            services.AddDbContext<TravelAppContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("TravelConnection"),
                        b => b.MigrationsAssembly(typeof(TravelAppContext).Assembly.FullName)),
                        ServiceLifetime.Singleton);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ITravelRepository, TravelRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }

    }
}