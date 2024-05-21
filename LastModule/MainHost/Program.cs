
using Common;
using DependencyInjectionAttribute;
using Microsoft.AspNetCore.Builder;
using ModuleLib;
using System.Reflection;

namespace MainHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var moduleManager = new ModuleManager();
            moduleManager.LoadModules(builder.Services);
            // Add services to the container.
            Assembly otherAssembly = Assembly.LoadFile("C:\\Users\\victor.liu\\Documents\\GitHub\\AspNetCoreSimpleAop\\LastModule\\FirstWeb\\bin\\Debug\\net8.0\\FirstWeb.dll"); //测试才这么写
            builder.Services.AddControllers().AddApplicationPart(otherAssembly);
            // 扫描并注册其他程序集中的服务
            ReisterServiceFromAssembly(builder.Services, otherAssembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddTransient<TestInterface, TestClass>();
            var app = builder.Build();
            ServiceLocator.Instance = app.Services;
            moduleManager.Configures(app);
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

        private static void ReisterServiceFromAssembly(IServiceCollection services, Assembly assembly)
        {
            var typesWithServiceAttribute = assembly.GetTypes().Where(t => t.GetCustomAttributes<CusServiceAttribute>().Any());

            foreach (var type in typesWithServiceAttribute)
            {
                var servceAttribute = type.GetCustomAttribute<CusServiceAttribute>();
                services.Add(new ServiceDescriptor(servceAttribute.ServiceType ?? type, type, servceAttribute.Lifetime));
            }
        }
    }
}
