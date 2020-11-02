using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CustomizablePageHandlers
{
    public class CustomHandlerPageApplicationModelProvider : IPageApplicationModelProvider
    {
        public void OnProvidersExecuting(PageApplicationModelProviderContext context)
        {
            var a = context;
        }

        public void OnProvidersExecuted(PageApplicationModelProviderContext context)
        {
            var a = context;
        }

        // The order is set to execute after the DefaultPageApplicationModelProvider.
        public int Order => -1000 + 10;
    }
}
