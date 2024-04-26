using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StartupDiagnostics
{
    public class BackStartupService : BackgroundService
    {
        private  FileSystemWatcher _watcher;

        private void OnDllChanged(object sender, FileSystemEventArgs e)
        {
            // DLL 文件发生更改时执行重新加载逻辑
            try
            {
                var dllPath = e.FullPath;
                var assembly = Assembly.LoadFrom(dllPath);
                // TODO: 实现重新加载逻辑
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to reload DLL: {ex.Message}");
            }
        }
        public BackStartupService(FileSystemWatcher watcher)
        {
            _watcher=watcher;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
               var _dllDirectory = "C:\\Users\\victor.liu\\Documents\\GitHub\\AspNetCoreSimpleAop\\AopLibraryTest\\lib";

                // 创建文件监视器
                _watcher = new FileSystemWatcher(_dllDirectory, "*.dll");
                _watcher.NotifyFilter = NotifyFilters.LastWrite;
                _watcher.IncludeSubdirectories = false;
                _watcher.Changed += OnDllChanged;
                _watcher.EnableRaisingEvents = true;
                await Task.Delay(3000, stoppingToken);
            }

            await Task.CompletedTask;
        }
    }
}
