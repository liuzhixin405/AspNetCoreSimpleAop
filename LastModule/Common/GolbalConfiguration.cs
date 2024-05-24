using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class GolbalConfiguration
    {
        /// <summary>
        /// 数据初始化时间
        /// </summary>
        public static DateTime InitialOn = new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Local);

        public static List<ModuleInfo> Modules { get; set; } = new List<ModuleInfo>();
       
        public static string WebRootPath { get; set; }

        public static string ContentRootPath { get; set; }

        public static IConfiguration Configuration { get; set; }

    }
}
