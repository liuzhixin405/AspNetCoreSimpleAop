using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AopLibrary.CusImplement
{
    internal class RootServiceFactory<T> : IRootServiceFactory<T>
    {
        private T _instance { get; set; }
        public T GetServiceInstance()
        {
            return _instance;
        }
        //rivate readonly IServiceProvider _serviceProvider;//没有业务需要
        public RootServiceFactory(T t)
        {
            _instance = t;
             aopList ??= new List<ISimpleAop>();
        }
        private List<ISimpleAop> aopList;

        public async Task<TResponse?> Invoke<TResponse>(string methodName, object?[]? args)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new Exception("需要调用的方法名不能为空");
            }
            var method = _instance.GetType().GetMethods().Where(x => x.DeclaringType.Name == _instance.GetType().Name).Where(x => x.Name.ToLower().Equals(methodName?.ToLower())).FirstOrDefault();
            if (method == null)
            {
                throw new Exception($"方法{methodName}不存在请检查");
            }
            var task = await InvokeCore(method, args);
            if (task.GetType().BaseType.Name == "Task")
                return await (Task<TResponse?>)task;
            else
                if (task is Task)
            {
                return await (task as Task<TResponse?>);
            }
            else
            {
                return (TResponse?)task;
            }
        }

        public async Task Invoke(string methodName, object?[]? args)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new Exception("需要调用的方法名不能为空");
            }
            var method = _instance.GetType().GetMethods().Where(x => x.DeclaringType.Name == _instance.GetType().Name).Where(x => x.Name.ToLower().Equals(methodName?.ToLower())).FirstOrDefault();
            if (method == null)
            {
                throw new Exception($"方法{methodName}不存在请检查");
            }
            await InvokeCore(method, args);
        }

        private async Task<object?> InvokeCore(MethodInfo? targetMethod, object?[]? args)
        {

            // 执行前置切面
            foreach (var aspect in aopList)
            {
                await aspect.Before(args);
            }

            // 执行目标方法
            var result = targetMethod.Invoke(_instance, args);

            // 执行后置切面
            foreach (var aspect in aopList)
            {
                result = await aspect.After(result);
            }

            aopList.Clear();
            return result;
        }

        public IRootServiceFactory<T> AddAop(ISimpleAop simpleAop)
        {
            aopList.Add(simpleAop); 
            return this;
        }
    }
}