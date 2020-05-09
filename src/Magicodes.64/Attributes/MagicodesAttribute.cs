using System;

namespace Magicodes._64.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class MagicodesAttribute : Attribute, IMagicodesData
    {
        /// <summary>
        ///     Model Type
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        ///     Template Path
        /// </summary>
        public string TemplatePath { get; set; }
    }
}
