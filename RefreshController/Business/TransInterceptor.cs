using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test001Controller
{
    public class TransInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Before executing {invocation.Method.Name}");

            invocation.Proceed();  // 调用实际方法

            Console.WriteLine($"After executing {invocation.Method.Name}");
        }
    }
}
