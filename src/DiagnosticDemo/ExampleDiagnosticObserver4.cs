using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;

namespace DiagnosticDemo
{
    public class ExampleDiagnosticObserver4 : IObserver<DiagnosticListener>
    {
        private readonly List<IDisposable> _subscriptions = new List<IDisposable>();
        private readonly AsyncLocal<Stopwatch> _stopwatch = new AsyncLocal<Stopwatch>();

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(DiagnosticListener value)
        {
            if (value.Name == "SqlClientDiagnosticListener")
            {
                var subscription = value.SubscribeWithAdapter(this);
                _subscriptions.Add(subscription);
            }
        }

        [DiagnosticName("System.Data.SqlClient.WriteCommandBefore")]
        public void OnCommandBefore()
        {
            _stopwatch.Value = Stopwatch.StartNew();
        }

        [DiagnosticName("System.Data.SqlClient.WriteCommandAfter")]
        public void OnCommandAfter(DbCommand command)
        {
            var stopwatch = _stopwatch.Value;
            stopwatch.Stop();
            Console.WriteLine($"CommandText: {command.CommandText}");
            Console.WriteLine($"Elapsed: {stopwatch.Elapsed}");
            Console.WriteLine();
        }
    }
}
