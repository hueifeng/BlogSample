using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace DotNetAspectCore
{
    /// <summary>
    ///     自定义拦截器
    /// </summary>
    public class CustomInterceptorAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        ///     实现抽象方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                Console.WriteLine("执行之前");
                await next(context);//执行被拦截的方法
            }
            catch (Exception)
            {
                Console.WriteLine("被拦截的方法出现异常");
                throw;
            }
            finally
            {
                Console.WriteLine("执行之后");
            }
        }
    }
}
