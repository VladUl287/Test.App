using Microsoft.EntityFrameworkCore;
using Test.Api.Configuration;
using Test.Core.Contracts.Repositories;
using Test.Core.Contracts.Services;
using Test.Core.Services;
using Test.Infrastructure.Repositories;

namespace Test.Api.Extentsions;

public static class ServicesExtensions
{
    public static void AddDatabase<TContext, TAssemblyMarker>(this IServiceCollection services, IConfiguration configuration)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(
            options =>
            {
                //options.UseInMemoryDatabase("db");
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    options =>
                    {
                        options.MigrationsAssembly(typeof(TAssemblyMarker).Assembly.FullName);
                    });
                options.LogTo(Console.WriteLine);
                options.EnableSensitiveDataLogging();
            },
            ServiceLifetime.Scoped
        );
    }

    public static void AddDefaultCors(this IServiceCollection services, IConfiguration configuration)
    {
        var corsConfig = configuration
            .GetSection(CorsConfig.Position)
            .Get<CorsConfig>();

        if (corsConfig is null or { Origins.Length: 0 })
        {
            throw new NullReferenceException("Cors configuration not found or not correct.");
        }

        services.AddCors(setup => setup
            .AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(corsConfig.Origins);
            })
        );
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEmployeePresenter, EmployeePresenter>();
        services.AddScoped<IEmployeeManager, EmployeeManager>();
        services.AddScoped<IDepartmentPresenter, DepartmentPresenter>();
    }    
}
