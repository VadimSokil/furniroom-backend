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

            var sqlRequests = new Dictionary<string, string>();
            foreach (var request in requestsSection.GetChildren())
            {
                sqlRequests[request.Key] = request.Value;
            }

            builder.Services.AddScoped<ICategoryService, CategoryService>(provider => new CategoryService(connectionString, sqlRequests));
            builder.Services.AddScoped<ISubcategoryService, SubcategoryService>(privider => new SubcategoryService(connectionString, sqlRequests));
            builder.Services.AddScoped<IProductTypeService, ProductTypeService>(provider => new ProductTypeService(connectionString, sqlRequests));
            builder.Services.AddScoped<IProductSubcategoryService, ProductSubcategoryService>(provider => new ProductSubcategoryService(connectionString, sqlRequests));
            builder.Services.AddScoped<IProductGalleryService, ProductGalleryService>(provider => new ProductGalleryService(connectionString, sqlRequests));
            builder.Services.AddScoped<IProductDrawingsService, ProductDrawingsService>(provider => new ProductDrawingsService(connectionString, sqlRequests));
            builder.Services.AddScoped<IAboutCompanyService, AboutCompanyService>(provider => new AboutCompanyService(connectionString, sqlRequests));
            builder.Services.AddScoped<IDeliveryService, DeliveryService>(provider => new DeliveryService(connectionString, sqlRequests));
            builder.Services.AddScoped<IPaymentService, PaymentService>(provider => new PaymentService(connectionString, sqlRequests));

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
