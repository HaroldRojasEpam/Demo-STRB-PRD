using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarbucksDemo.Infraestructure.Data.Models;
using Testcontainers.MsSql;
using Environment = StarbuckTestDemo.API.Integration.Helper.Environment;

namespace StarbuckTestDemo.API.Integration
{
    public class IntegrationWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _container;

        public IntegrationWebApplicationFactory() 
        {
            _container = new MsSqlBuilder().Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var connnectionString = _container.GetConnectionString();
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)); 
                if(descriptor is not null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(connnectionString);
                });
            });
            base.ConfigureWebHost(builder);
        }

        public Task InitializeAsync()
        {
            return _container.StartAsync();
        }

        public new Task DisposeAsync()
        {
            return _container.StopAsync();
        }
    }
}
