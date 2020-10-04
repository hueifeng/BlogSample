using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;

namespace DiagnosticDemo
{
    public class ExampleDiagnosticObserver2 : IObserver<DiagnosticListener>,
         IObserver<KeyValuePair<string, object>>
    {
        private readonly List<IDisposable> _subscriptions = new List<IDisposable>();
        private readonly AsyncLocal<Stopwatch> _stopwatch = new AsyncLocal<Stopwatch>();

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            Write(value.Key, value.Value);
        }

        public void OnNext(DiagnosticListener value)
        {
            if (value.Name == "SqlClientDiagnosticListener")
            {
                var subscription = value.Subscribe(this);
                _subscriptions.Add(subscription);
            }
        }

        private void Write(string name, object value)
        {
            switch (name)
            {
                case "System.Data.SqlClient.WriteCommandBefore":
                    {
                        _stopwatch.Value = Stopwatch.StartNew();
                        break;
                    }
                case "System.Data.SqlClient.WriteCommandAfter":
                    {
                        var stopwatch = _stopwatch.Value;
                        stopwatch.Stop();
                        var command = GetProperty<SqlCommand>(value, "Command");
                        Console.WriteLine($"CommandText: {command.CommandText}");
                        Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");
                        Console.WriteLine();
                        break;
                    }
            }
        }

        private static T GetProperty<T>(object value, string name)
        {
            return (T)value.GetType()
                .GetProperty(name)
                .GetValue(value);
        }
    }
}
