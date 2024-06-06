
using AuthorizationWithMiddleware.Data;
using AuthorizationWithMiddleware.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AuthorizationWithMiddleware
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
            
            builder.Services.AddScoped<IUserService, UserService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthorizationWithMiddleware API", Version = "v1" });
                c.OperationFilter<AddRequiredHeaderParameter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<ApiKeyMiddleware>();



            app.MapControllers();

            app.Run();
        }
    }
}
