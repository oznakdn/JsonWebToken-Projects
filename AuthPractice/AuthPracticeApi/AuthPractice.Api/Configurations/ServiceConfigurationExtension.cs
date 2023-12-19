using AuthPractice.Api.Data;
using AuthPractice.Api.Repositories.Concretes;
using AuthPractice.Api.Repositories.Contracts;
using AuthPractice.Api.Security;
using AuthPractice.Api.Services.Concretes;
using AuthPractice.Api.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthPractice.Api.Configurations;
public static class ServiceConfigurationExtension
{

    public static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(configuration.GetConnectionString("AppConnection")));
        return services;
    }

    public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(scheme =>
        {
            scheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            scheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new()
            {
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidIssuer = configuration["TokenSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["TokenSettings:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenSettings:SecurityKey"]))
            };
        });

        return services;
    }

    public static IServiceCollection AddContainerService(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}

