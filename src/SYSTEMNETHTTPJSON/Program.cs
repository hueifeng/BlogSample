using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace SYSTEMNETHTTPJSON
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureWebHost(builder =>
                {
                    builder.UseKestrel()
                    .Configure(app =>
                    {

                      
                        app.Map("/customers", (config) =>
                        {
                            config.Run(async (context) =>
                            {
                                context.Response.ContentType = "application/json";
                                await context.Response.WriteAsync(JsonSerializer.Serialize(new Customer(){Id = "1",Name = "fH"}));
                            });
                        });
                        app.Map("/create", (config) =>
                        {
                            config.Run(async (context) =>
                            {
                                await context.Response.WriteAsync("2");
                            });
                        });
                        app.Run(async (context) =>
                        {
                            await GetCustomerAsync();
                            await CreateCustomerAsync();
                            await context.Response.WriteAsync("Hello");
                        });

                    });
                })
                .Build();
            await host.RunAsync();
         

        }


        public static async Task<Customer> GetCustomerAsync()
        {
            HttpClient clinet=new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5000/customers");
            var response = await clinet.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<Customer>();
        }

        public static async Task<Customer> CreateCustomerAsync()
        {
            HttpClient clinet = new HttpClient();
            var customer=new Customer()
            {
                Id = "1",
                Name = "Fh"
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/create");
            request.Content = JsonContent.Create(customer);
            var response = await clinet.SendAsync(request);
            var content=response.Content.ReadAsStringAsync();
            return customer;
        }




    }
}
