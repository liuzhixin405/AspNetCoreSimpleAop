using IDynamicServiceBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControllerService
{
    public static class Extensions
    {
        public static string GetFullName(this IUserService service)
        {
            return "success";
        }
    }
}
