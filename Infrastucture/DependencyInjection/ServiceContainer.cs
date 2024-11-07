using Microsoft.EntityFrameworkCore;
using Application.Extension.Identity;
using Infrastucture.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Application.Interface.Identity;
using Infrastucture.Repository;
using Infrastucture.Repository.Products.Handlers.Products;


namespace Infrastucture.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(config.GetConnectionString("AppDB")), ServiceLifetime.Scoped);
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies();
            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.AddAuthorizationBuilder()
                .AddPolicy("AdministrationPolicy", adp =>
                {
                    adp.RequireAuthenticatedUser();
                    adp.RequireRole("Admin", "Manager");
                })
                .AddPolicy("UserPolicy", adp =>
                {
                    adp.RequireAuthenticatedUser();
                    adp.RequireRole("User");
                });
            services.AddCascadingAuthenticationState();
            services.AddScoped<IAccount, Account>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateProductHandler).Assembly));
            services.AddScoped<DataAccess.IDbContextFactory<AppDbContext>, DbContextFactory<AppDbContext>>();
            return services;
        }
    }
}
