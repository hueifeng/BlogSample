using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using DotNetAspectCore.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNetAspectCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICustomService,CustomService>();
            services.AddMvc();
            // services.AddTransient<CustomInterceptorAttribute>(provider => new CustomInterceptorAttribute());
            //全局拦截器。使用AddDynamicProxy(Action<IAspectConfiguration>)的重载方法，其中IAspectConfiguration提供Interceptors注册全局拦截器:
            //services.ConfigureDynamicProxy(config =>
            //{
            //    config.Interceptors.AddTyped<CustomInterceptorAttribute>();
            //});
            //services.ConfigureDynamicProxy(config =>
            //{
            //    config.Interceptors.AddTyped<CustomInterceptorAttribute>(method => method.DeclaringType.Name.EndsWith("Service"));
            //});
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            return services.BuildAspectInjectorProvider();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
