using FurniroomAPI.Interfaces;
using FurniroomAPI.Services;

namespace FurniroomAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            var endpointsSection = configuration.GetSection("Endpoints");

            var endpointURL = new Dictionary<string, string>();
            foreach (var request in endpointsSection.GetChildren())
            {
                endpointURL[request.Key] = request.Value;
            }

            builder.Services.AddHttpClient<ICategoryService, CategoryService>();
            builder.Services.AddHttpClient<ISubcategoryService, SubcategoryService>();
            builder.Services.AddSingleton(endpointURL);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("AllowAll");

            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            app.Urls.Add($"http://0.0.0.0:{port}");

            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Furniroom API");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
