# dotnetcore实现Aop

```
   Aop大家都不陌生,然而今天给大家不将讲官方的filter,今天给大家分享一个轻量级的Aop解决方案(AspectCore)
```

> 什么是AspectCore

AspectCore是一个面向切面编程,基于.NetCore和.NetFramwork的扩平台框架,对方法拦截器、依赖项注入集成、web应用程序、数据验证等提供核心支持。

> AspectCore基本特性

- 提供抽象的Aop接口,基于该接口可以轻松的使用自己的代理类实现替换默认的实现.

- 框架不包含IoC，也不依赖具体IoC实现,可以使用Asp.Net Core的内置依赖注入或者任何兼容Asp.Net Core的第三方Ioc来继承AspectCore到Asp.NetCore应用中

- 高性能的异步拦截系统

- 灵活的配置系统

- 基于service的而非基于实现类的切面构造

- 支持扩平台的Asp.Net Core环境

  > 使用AspectCore

  从NuGet中安装AspectCore

  ```c
  AspectCore.Extensions.DependencyInjection
  ```

  package

  ```
  PM> Install-package AspectCore.Extensions.DependencyInjection
  ```

  下面我创建了一个Api应用程序.

  NuGet安装

  ```
  AspectCore.Configuration
  ```

  package

  ```
  PM> Install-package AspectCore.Configuration
  ```

  下面我新建了一个拦截器 CustomInterceptorAttribute,继承AbstractInterceptorAttribute(一般情况下继承他即可),他实现IInterceptor接口AspectCore默认实现了基于`Attribute`的拦截器配置。

  ```c#
  /// <summary>
  ///     自定义拦截器
  /// </summary>
  public class CustomInterceptorAttribute : AbstractInterceptorAttribute
  {
      /// <summary>
      ///     实现抽象方法
      /// </summary>
      /// <param name="context"></param>
      /// <param name="next"></param>
      public override async Task Invoke(AspectContext context, AspectDelegate next)
      {
          try
          {
              Console.WriteLine("执行之前");
              await next(context);//执行被拦截的方法
          }
          catch (Exception)
          {
              Console.WriteLine("被拦截的方法出现异常");
              throw;
          }
          finally
          {
              Console.WriteLine("执行之后");
          }
      }
  }
  ```

  定义`ICustomService`接口和它的实现类`CustomService`:

  ```C#
  public interface ICustomService
  {
      DateTime GetDateTime();
  }
  public class CustomService : ICustomService
  {
      public DateTime GetDateTime()
      {
          return DateTime.Now;
  
       }
  }
  ```

  在ValuesController注入ICustomService

  ```c#
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
      private readonly ICustomService _icustomserveice;
      public ValuesController(ICustomService icustomService) {
          this._icustomserveice = icustomService;
      }
  
      // GET api/values
      [HttpGet]
      public DateTime Get()
      {
          return _icustomserveice.GetDateTime();
      }
  
  }
  ```

  注册ICustomService,并创建代理容器

  ```C#
   public IServiceProvider ConfigureServices(IServiceCollection services)
          {
              services.AddTransient<ICustomService,CustomService>();
              services.AddMvc();
              //全局拦截器。使用ConfigureDynamicProxy(Action<IAspectConfiguration>)的重载方法，其中IAspectConfiguration提供Interceptors注册全局拦截器:
              services.ConfigureDynamicProxy(config=> {
                  config.Interceptors.AddTyped<CustomInterceptorAttribute>();
              });
              services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
              return services.BuildAspectInjectorProvider();
       }
  ```

  作为服务的全局拦截器。在`ConfigureServices`中添加：

  ```c#
  services.AddTransient<CustomInterceptorAttribute>(provider => new CustomInterceptorAttribute());
  ```

  作用于特定`Service`或`Method`的全局拦截器，下面的代码演示了作用于带有`Service`后缀的类的全局拦截器：

  ```C#
   services.ConfigureDynamicProxy(config =>
              {
                  config.Interceptors.AddTyped<CustomInterceptorAttribute>(method => method.DeclaringType.Name.EndsWith("Service"));
              });
  ```

  通配符拦截器,匹配后缀为Service

  ```C#
   services.ConfigureDynamicProxy(config =>
              {
                  config.Interceptors.AddTyped<CustomInterceptorAttribute>(Predicates.ForService("*Service"));
              });
  ```

  在AspectCore中提供`NonAspectAttribute`来使得`Service`或`Method`不被代理：

  ```C#
     [NonAspect]
      DateTime GetDate();
  ```

  全局配置忽略条件

```C#
      services.ConfigureDynamicProxy(config =>
        {
            //Namespace命名空间下的Service不会被代理
            config.NonAspectPredicates.AddNamespace("Namespace");
            //最后一级为Namespace的命名空间下的Service不会被代理
            config.NonAspectPredicates.AddNamespace("*.Namespace");
            //ICustomService接口不会被代理
            config.NonAspectPredicates.AddService("ICustomService");
            //后缀为Service的接口和类不会被代理
            config.NonAspectPredicates.AddService("*Service");
            //命名为Method的方法不会被代理
            config.NonAspectPredicates.AddMethod("Method");
            //后缀为Method的方法不会被代理
            config.NonAspectPredicates.AddMethod("*Method");
        });
```

AspectCore

https://github.com/dotnetcore/AspectCore-Framework

测试项目地址
https://github.com/fhcodegit/DotNetAspectCore/tree/master


