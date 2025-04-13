using BasicAuthenticationDemo.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using BasicAuthenticationDemo.Models.Interfaces;

namespace BasicAuthenticationDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавляем сервисы

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // Используем имена свойств, определенные в модели
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            // http://localhost:12300/swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Добавляем контекст БД
            builder.Services.AddDbContext<UserDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("EFCoreDBConnection")));

            // Регистрируем сервисы UserService, DeviceService
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDeviceService, DeviceService>();


            // Добавляем аутентификацию через BasicAuthenticationHandler
            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
