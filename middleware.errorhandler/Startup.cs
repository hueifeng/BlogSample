using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MediatR.Pipeline;
using MediatR;
using System.Reflection;
using Autofac;

namespace middleware.errorhandler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services1)
        {
            var services = new ServiceCollection();
            services1.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // services.AddScoped<ICustomer, Customer>();
            // services.AddMediatR(typeof(Customer));
            //  var provider = services.BuildServiceProvider();
            services.AddMediatR(typeof(IMediator).GetTypeInfo().Assembly);
            // return provider.GetRequiredService<IMediator>();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseExceptionHandler(errorApp => errorApp.Run(async context => {
            //    // use Exception handler path feature to catch the exception details   
            //    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            //    // log errors using above exceptionHandlerPathFeature object   
            //    Console.WriteLine(exceptionHandlerPathFeature?.Error);
            //    // Write a custom response message to API Users   
            //    context.Response.StatusCode = 500;
            //    // Set a response format    
            //    context.Response.ContentType = "application/json";
            //    await context.Response.WriteAsync("error occured.");
            //}));
            //app.UseMediatR();
            app.UseMvc();
        }
    }
}
