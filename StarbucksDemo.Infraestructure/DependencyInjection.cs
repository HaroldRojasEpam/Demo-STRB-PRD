using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarbucksDemo.Application.Users;
using StarbucksDemo.Infraestructure.Data.Models;
using StarbucksDemo.Infraestructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbucksDemo.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration) {
            string? configurationString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configurationString)
            );

            services.AddScoped<UserRepository>();

            services.AddScoped<IUsersRepository>(u => 
                u.GetRequiredService<UserRepository>()
            );

            return services;
        }
    }
}
