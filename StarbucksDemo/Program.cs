using StarbucksDemo.Extensions;

var builder = WebApplication.CreateBuilder(args).ConfigurateBuilder();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build().Configurate();

app.Run();

public partial class Program { }