using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DiagnosticDemo
{
    public class ExampleDiagnosticObserver1 : IObserver<DiagnosticListener>,
         IObserver<KeyValuePair<string, object>>
    {
        private readonly List<IDisposable> _subscriptions = new List<IDisposable>();

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
            Console.WriteLine(name);
            Console.WriteLine(value);
            Console.WriteLine();
        }
    }
}
