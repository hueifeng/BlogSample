using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OptionsConfigure
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

              services.Configure<MyOptions>(Configuration.GetSection("MyOptions"));
            ////基础注册方式
            //services.Configure<MyOptions>(o => { o.Url = "MyOptions"; o.Name = "Name111"; });
            ////指定具体名称
            //services.Configure<MyOptions>("Option", o => { o.Url = "MyOptions"; o.Name = "Name111"; }) ;
            ////配置所有实例
            //services.ConfigureAll<MyOptions>(options =>{ options.Name = "Name1";  options.Url = "Url1";});

            //services.PostConfigure<MyOptions>(o => o.Name = "Name1");
            //services.PostConfigure<MyOptions>("Option", o => o.Name = "Name1");
            //services.PostConfigureAll<MyOptions>(o => o.Name = "Name1");


            //// 使用配置文件来注册实例
            //services.Configure<MyOptions>(Configuration.GetSection("MyOptions"));
            //// 指定具体名称
            //services.Configure<MyOptions>("Option", Configuration.GetSection("MyOptions"));


            services.AddControllers();

            //services.ConfigureAll<MyOptions>(options =>
            //{
            //    // options.Name = "Name1";
            //    options.Url = "Url1";
            //});

            //services.PostConfigure<MyOptions>("Option", o => { o.Url = "MyOptions"; o.Name = "Name111"; });
            //services.PostConfigureAll<MyOptions>(options =>
            //{
            //    // options.Name = "Name1";
            //    options.Url = "Url1";
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
