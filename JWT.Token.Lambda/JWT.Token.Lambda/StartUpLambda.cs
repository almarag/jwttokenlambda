namespace JWT.Token.Lambda
{
    using JWT.Token.Lambda.Entities;
    using JWT.Token.Lambda.Interfaces;
    using JWT.Token.Lambda.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;

    public class StartUpLambda : IStartUpLambda
    {
        public IServiceCollection AddLambdaServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            var jwtConfig = configuration.GetSection("JwtSettings");

            var jwtSettings = new JwtSettings()
            {
                Audience = jwtConfig["Audience"],
                ExpirationDays = Convert.ToInt32(jwtConfig["ExpirationDays"]),
                Issuer = jwtConfig["Issuer"],
                JwtAdminCredentials = new JwtAdminCredentials()
                {
                    UserName = jwtConfig["JwtAdminCredentials:UserName"],
                    Password = jwtConfig["JwtAdminCredentials:Password"],
                },
                Secret = jwtConfig["Secret"]                
            };

            services.AddTransient(c => jwtSettings);

            var connection = configuration.GetSection("ConnectionStrings");
            services.AddDbContext<jwtContext>(
                options => options.UseSqlite(connection["JwtDbContext"])
            );

            return services;
        }

        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                #if DEBUG
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                #endif
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            return configuration;
        }
    }
}
