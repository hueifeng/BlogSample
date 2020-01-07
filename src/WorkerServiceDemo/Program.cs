using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace WorkerServiceDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            #region Windows

            //return Host.CreateDefaultBuilder(args)

            //    .ConfigureServices((hostContext, services) =>
            //    {
            //        services.AddHostedService<Worker>();
            //    }).UseWindowsService();

            #endregion

            #region Linux
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                }).UseSystemd();



            #endregion





        }
    }
}
