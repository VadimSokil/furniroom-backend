using InformationService.Services;
using InformationService.Interfaces;

namespace InformationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = Environment.GetEnvironmentVariable("connectionString");

            var configuration = builder.Configuration;
            var requestsSection = configuration.GetSection("Requests");

            var requests = new Dictionary<string, string>();
            foreach (var request in requestsSection.GetChildren())
            {
                requests[request.Key] = request.Value;
            }

            builder.Services.AddScoped<ICompanyService, CompanyService>(provider => new CompanyService(connectionString, requests));
            builder.Services.AddScoped<IProductsService, ProductsService>(provider => new ProductsService(connectionString, requests));
            

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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Information service");
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
