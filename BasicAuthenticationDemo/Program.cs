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

            // ��������� �������

            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // ���������� ����� �������, ������������ � ������
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            // http://localhost:12300/swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ��������� �������� ��
            builder.Services.AddDbContext<UserDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("EFCoreDBConnection")));

            // ������������ ������� UserService, DeviceService
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDeviceService, DeviceService>();


            // ��������� �������������� ����� BasicAuthenticationHandler
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
