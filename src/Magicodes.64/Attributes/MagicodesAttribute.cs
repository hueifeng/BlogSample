using System;

namespace Magicodes._64.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class MagicodesAttribute : Attribute, IMagicodesData
    {
        public Type Type { get; set; }
    }
}
