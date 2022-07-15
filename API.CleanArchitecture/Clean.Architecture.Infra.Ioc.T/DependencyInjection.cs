using Clean.Architecture.Application.Interfaces;
using Clean.Architecture.Application.Mappings;
using Clean.Architecture.Application.Services;
using Clean.Architecture.Domain.Account;
using Clean.Architecture.Domain.Interfaces;
using Clean.Architecture.Infra.Data.Context;
using Clean.Architecture.Infra.Data.Identity;
using Clean.Architecture.Infra.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Clean.Architecture.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                                  options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                  b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            //serviço de redirecionamento do usuario caso ele nao tenha feito login
            services.ConfigureApplicationCookie(option => option.AccessDeniedPath = "/Account/Login");

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            var myHandlers = AppDomain.CurrentDomain.Load("Clean.Architecture.Application");
            services.AddMediatR(myHandlers);

            return services;
        }
    }
}
