using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DebuggerDisplayAttributes
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample sample = new Sample(){Name = "HueiFeng"};
            Console.ReadLine();
        }
    }

    [DebuggerTypeProxy(typeof(SampleDebugView))]
    public class Sample
    {
        public string Name { get; set; }

        private class SampleDebugView
        {
            private readonly Sample _sample;

            public SampleDebugView(Sample sample)
            {
                _sample = sample;
            }

            public string Name => _sample.Name;
            public int NameLength => _sample.Name.Length;

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public char[] NameCharacters => _sample.Name.ToCharArray();
        }
    }
}
