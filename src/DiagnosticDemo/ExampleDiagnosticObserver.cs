using System;
using System.Diagnostics;

namespace DiagnosticDemo
{
    public sealed class ExampleDiagnosticObserver : IObserver<DiagnosticListener>
    {
        public void OnCompleted()
        {  
        }

        public void OnError(Exception error)
        {  
        }

        public void OnNext(DiagnosticListener value)
        {
            Console.WriteLine(value.Name);
        }
    }
}
