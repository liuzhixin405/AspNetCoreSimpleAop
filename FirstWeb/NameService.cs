using DependencyInjectionAttribute;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWeb
{
    //[CusService(ServiceLifetime.Transient)]  无接口
    [CusService(typeof(IService), ServiceLifetime.Transient)]
    public class NameService : IService
    {
        public string Name()
        {
            return "success";
        }
    }
}
