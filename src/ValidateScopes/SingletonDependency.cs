using System;
using Microsoft.Extensions.DependencyInjection;

namespace ValidateScopes
{
    public class SingletonDependency
    {
        //private readonly ScopedDependency _scopedDepdendency;
        private readonly IServiceProvider _serviceProvider;
        public SingletonDependency(
            //ScopedDependency scopedDepdendency
            IServiceProvider serviceProvider
        )
        {
            //this.scopedDepdendency = scopedDepdendency;
            this._serviceProvider = serviceProvider;
        }

        public int GetNextCounter()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedDependency = scope.ServiceProvider.GetRequiredService<ScopedDependency>();
                return scopedDependency.GetNextCounter();
            }
        }
    }
}