using DynamicServiceBusiness;
using DynamicServiceDemo.AssemblyExtensions;
using IDynamicServiceBusiness;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DynamicServiceDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

         
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add services to the container.
         
            builder.Services.AddControllers().ConfigureApplicationPartManager(apm =>
            {
                var context = new CollectibleAssemblyLoadContext();
                DirectoryInfo DirInfo = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "lib"));
                foreach (var file in DirInfo.GetFiles("*.dll"))
                {

                    if (!(file.Name.Contains("UserControllerService")))
                    {
                        continue;
                    }//�������ε���������
                    var assembly = context.LoadFromAssemblyPath(file.FullName);
                    var controllerAssemblyPart = new AssemblyPart(assembly);
                    apm.ApplicationParts.Add(controllerAssemblyPart);
                    ExternalContexts.Add(file.Name.Replace(".dll", ""), context);
                  
                }
            });
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddSingleton<IActionDescriptorChangeProvider>(ActionDescriptorChangeProvider.Instance);
            builder.Services.AddSingleton(ActionDescriptorChangeProvider.Instance);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthorization();

            app.MapGet("/Add", ([FromServices] ApplicationPartManager _partManager, string name) =>
            {

                //FileInfo FileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "lib/" + name + ".dll"));
                FileInfo FileInfo = new FileInfo("D:\\github\\AspNetCoreSimpleAop\\ExtensionPattern\\DynamicServiceDemo\\lib\\bak\\UserControllerService.dll"); //����add��remove�ǿ��Է�һ���ļ��еģ�������Ҫ΢�����룬����Ϊ����ʾ���㣬��
                using (FileStream fs = new FileStream(FileInfo.FullName, FileMode.Open))
                {
                    var context = new CollectibleAssemblyLoadContext();
                    var assembly = context.LoadFromStream(fs);
                    var controllerAssemblyPart = new AssemblyPart(assembly);

                    _partManager.ApplicationParts.Add(controllerAssemblyPart);

                    //ExternalContexts.Add(name + ".dll", context);
                    ExternalContexts.Add(name, context);

                    //����Controllers
                    ActionDescriptorChangeProvider.Instance.HasChanged = true;
                    ActionDescriptorChangeProvider.Instance.TokenSource!.Cancel();
                }
                return "���{name}controller�ɹ�";
            })
.WithTags("Main")
.WithOpenApi();

            app.MapGet("/Remove", ([FromServices] ApplicationPartManager _partManager, string name) =>
            {
                //if (ExternalContexts.Any(
                //    $"{name}.dll"))
                if (ExternalContexts.Any($"{name}"))
                {
                    var matcheditem = _partManager.ApplicationParts.FirstOrDefault(x => x.Name == name);
                    if (matcheditem != null)
                    {
                        _partManager.ApplicationParts.Remove(matcheditem);
                        matcheditem = null;
                    }
                    ActionDescriptorChangeProvider.Instance.HasChanged = true;
                    ActionDescriptorChangeProvider.Instance.TokenSource!.Cancel();
                    //ExternalContexts.Remove(name + ".dll");
                    ExternalContexts.Remove(name);
                    return $"�ɹ��Ƴ�{name}controller";
                }
                else
                {
                    return "$û��{name}controller";
                }
            });

            app.MapControllers();

            app.Run();
        }
    }
}
