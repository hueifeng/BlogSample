using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAspectCore.Service
{
    /// <summary>
    ///     接口实现
    /// </summary>
    public class CustomService : ICustomService
    {
        public DateTime GetDate()
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime()
        {
            return DateTime.Now;

        }
    }
}
