using StarbucksDemo.Routes;
using System.Globalization;

namespace StarbucksDemo.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication Configurate(this WebApplication app) {
            if (app.Environment.IsDevelopment())
            {
                TextInfo infoText = CultureInfo.CurrentCulture.TextInfo;
                app.UseSwagger();
                app.UseSwaggerUI(x => x.SwaggerEndpoint(
                        "/swagger/v1/swagger.json",
                        $"Starbucks Demo API - {infoText.ToTitleCase(app.Environment.EnvironmentName)} v1"
                    )
                );
                app.ApplyMigrations();
            }
            app.UseHsts();
            app.UseHttpsRedirection();
            app.MapUserRoutes();
            return app;
        }
    }
}
