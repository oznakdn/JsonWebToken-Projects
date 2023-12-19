using Jwt.Api.Data.Context;
using Jwt.Api.Middlewares;
using Jwt.Api.Repositories.Abstracts;
using Jwt.Api.Repositories.Concretes;
using Jwt.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Jwt.Api.Configurations;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(option => option.UseSqlite(configuration.GetConnectionString("CustomerDbConnection")));
        return services;
    }

    public static IServiceCollection AddCorsService(this IServiceCollection services)
    {
        services.AddCors(conf => conf.AddPolicy("Default", conf => {
            conf.AllowAnyHeader();
            conf.AllowAnyMethod();
            conf.WithOrigins("https://localhost:7003", "http://localhost:5081");
        }));
        return services;
    }

    public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(scheme => {

            scheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options => {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["TokenSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["TokenSettings:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenSettings:SigningKey"])),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

        });

        return services;
    }

    public static IServiceCollection AddServiceContainer(this IServiceCollection services)
    {
        services.AddScoped<ITokenHelper, TokenHelper>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddTransient<GlobalExceptionMiddleware>();
        return services;
    }
}
