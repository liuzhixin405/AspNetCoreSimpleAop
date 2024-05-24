
using Common;
using DependencyInjectionAttribute;
using MainHost.ServiceExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using ModuleLib;
using System.Reflection;

namespace MainHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
            builder.Configuration.AddJsonFile("appsettings.Modules.json", optional: false, reloadOnChange: true);
            //var moduleManager = new ModuleManager();

            //moduleManager.LoadModules(builder.Services);
            // Add services to the container.
            builder.Services.InitModule(builder.Configuration);
            var sp = builder.Services.BuildServiceProvider();
            var moduleInitializers = sp.GetServices<IModule>();
            foreach (var moduleInitializer in moduleInitializers)
            {
                moduleInitializer.ConfigureService(builder.Services, builder.Configuration);
            }
            var assemblys =GolbalConfiguration.Modules.Select(x => x.Assembly).ToList();
            //Assembly otherAssembly = Assembly.LoadFile("C:\\Users\\victor.liu\\Documents\\GitHub\\AspNetCoreSimpleAop\\LastModule\\FirstWeb\\bin\\Debug\\net8.0\\FirstWeb.dll"); //测试才这么写
            var mvcBuilder = builder.Services.AddControllers();
            foreach (var assembly in assemblys)
            {
                // 扫描并注册其他程序集中的控制器
                mvcBuilder.AddApplicationPart(assembly);
                // 扫描并注册其他程序集中的服务
                builder.Services.ReisterServiceFromAssembly(assembly);
            }
           

            
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddTransient<TestInterface, TestClass>();
            var app = builder.Build();
            ServiceLocator.Instance = app.Services;

            foreach (var moduleInitializer in moduleInitializers)
            {
                moduleInitializer.Configure(app,app.Environment);
            }
            //moduleManager.Configures(app);
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
