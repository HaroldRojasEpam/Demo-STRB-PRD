using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StarbucksDemo.Infraestructure.Data.Models;

namespace StarbuckTestDemo.API.Integration
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationWebApplicationFactory>
    {
        private readonly IServiceScope _scope;
        protected readonly ISender _sender;
        protected readonly ApplicationDbContext _dbContext;
        protected BaseIntegrationTest(IntegrationWebApplicationFactory factory)
        {
            _scope = factory.Services.CreateScope();
            _sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }
    }
}
