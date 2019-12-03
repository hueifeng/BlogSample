using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheckBlogDemo
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
            services.AddControllers();
            services.AddHealthChecks()  
          .AddCheck<DatabaseHealthCheck>("sql").AddApplicationInsightsPublisher(); ;
            services.AddHealthChecksUI();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app
       .UseHealthChecks("/health", new HealthCheckOptions
       {
           Predicate = _ => true
       });
            app
                 .UseRouting()
                 .UseEndpoints(config =>
                 {
                     //config.MapHealthChecks("/health", new HealthCheckOptions
                     //{
                     //    Predicate = _ => true,
                     //   //ResultStatusCodes = new Dictionary<HealthStatus, int> { { HealthStatus.Unhealthy, 420 }, { HealthStatus.Healthy, 200 }, { HealthStatus.Degraded, 419 } }
                     //   ResponseWriter =CustomResponseWriter
                        
                     //}); 

                 });
            app.UseHealthChecksUI();
        }
        public class DatabaseHealthCheck : IHealthCheck
        {
            public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken =
             default)
            {
                using (var connection = new SqlConnection("Server=111.;Initial Catalog=master;Integrated Security=true"))
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (SqlException)
                    {
                        return  Task.FromResult(HealthCheckResult.Unhealthy());
                    }
                }

                return Task.FromResult(HealthCheckResult.Healthy());

            }
        }
        public class RandomHealthCheck
           : IHealthCheck
        {
            public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
            {
                //if (DateTime.UtcNow.Minute % 2 == 0)
                //{
                //    return Task.FromResult(HealthCheckResult.Healthy());
                //}

                return Task.FromResult(HealthCheckResult.Unhealthy(description: "failed"));
            }
        }

        private static Task CustomResponseWriter(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json";

            var result = JsonConvert.SerializeObject(new
            {
                status = healthReport.Status.ToString(),
                errors = healthReport.Entries.Select(e => new
                {
                    key = e.Key,
                    value = e.Value.Status.ToString()
                })
            });
            return context.Response.WriteAsync(result);

        }
    }
}
