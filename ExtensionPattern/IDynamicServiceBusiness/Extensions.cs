using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDynamicServiceBusiness
{
    public static class Extensions
    {
        public static void Log(this IUserService service, string message)
        {
            Console.WriteLine(message);
        }
    }
}
