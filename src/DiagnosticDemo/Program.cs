using Dapper;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DiagnosticDemo
{
    class Program
    {
        public const string ConnectionString =
            @"Server=localhost;Database=master;Trusted_Connection=True;";
        static async Task Main(string[] args)
        {
            var observer = new ExampleDiagnosticObserver4();
            IDisposable subscription = DiagnosticListener.AllListeners.Subscribe(observer);
            var result = await Get();
            Console.WriteLine(result);

        }
        public static async Task<int> Get() {
            using (var connection=new SqlConnection(ConnectionString))  
            {
                return await connection.QuerySingleAsync<int>("SELECT 42;");
            }
        }
    }
}
