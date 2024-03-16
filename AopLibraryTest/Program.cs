using AopLibrary;
using AopLibrary.CusImplement;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace AopLibraryTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
      
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSimpleAop();
            builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
            builder.Services.AddSingleton(typeof(ISimpleAop), typeof(DefaultAOP));
            builder.Services.AddSingleton<ISimpleAop, LogAop>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
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