using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using StarbucksDemo.Application;
using StarbucksDemo.Infraestructure;

namespace StarbucksDemo.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder ConfigurateBuilder(this WebApplicationBuilder builder) {

            builder.Services.Configure<JsonOptions>(options => {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.SerializerOptions.PropertyNameCaseInsensitive = true;
                options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Starbucks Demo",
                        Description = "The demo, created by Harold Rojas, contains the MinimalAPI CRUD for users.",
                    }
                );
                options.DocInclusionPredicate((name, api) => true);
            });

            builder.Services.AddApplication();
            builder.Services.AddInfraestructure(builder.Configuration);
            return builder;
        }
    }
}
