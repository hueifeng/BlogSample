using AspectCore.DynamicProxy;
using System;

namespace DotNetAspectCore.Service
{
    public interface ICustomService
    {
        DateTime GetDateTime();
        [NonAspect]
        DateTime GetDate();
    }
}
