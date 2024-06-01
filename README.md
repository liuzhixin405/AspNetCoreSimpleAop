# AspNetCoreSimpleAop
 
 没有借助第三方组件实现的一个超级简单的Aop组件
对应博客
https://www.cnblogs.com/morec/p/17249940.html

该项目新增动态插件功能
1. repository和plugin这两个模块通过反射来动态获取，已实现dll热拔插，不太完美，算是模块发开发
2. AopLibraryTest程序集注入功能,autofac注入
3. Lastmodule模块化等开发示例 ,重点是controller及业务模块相互独立解耦  模块化开发推荐 
4. RefreshController动态加载Controller，不需要重启Host主机     完全模块化包括控制器热拔插开发推荐(唯一缺点：该实现服务层是通过反射来绕过服务层改变后需要注入问题)
5. ExtensionPattern动态加载Controller，不需要重启Host主机，通过使用接口扩展绕过服务层改变后需要重新注入问题  推荐,没有任何反射
