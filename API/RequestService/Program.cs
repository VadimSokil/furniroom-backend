using RequestService.Services;
using RequestService.Interfaces;

namespace RequestService
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

            builder.Services.AddScoped<IOrdersService, OrdersService>(provider => new OrdersService(connectionString, sqlRequests));
            builder.Services.AddScoped<IQuestionsService, QuestionsService>(privider => new QuestionsService(connectionString, sqlRequests));

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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Request service");
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
