using Microsoft.EntityFrameworkCore;
using StarbucksDemo.Infraestructure.Data.Models;

namespace StarbucksDemo.Extensions
{
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this WebApplication app) {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
