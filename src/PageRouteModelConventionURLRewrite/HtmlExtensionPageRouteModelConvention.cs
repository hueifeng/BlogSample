using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging;
using System.Text;

namespace PageRouteModelConventionURLRewrite
{
    public class HtmlExtensionPageRouteModelConvention : IPageRouteModelConvention
    {
        private readonly ILogger _logger;
        public HtmlExtensionPageRouteModelConvention(ILogger logger)
        {
            _logger = logger;
        }
        public void Apply(PageRouteModel model)
        {
            var log = new StringBuilder();
            log.AppendLine("====================================================");
            log.AppendLine($"Count：{model.Selectors.Count} ViewEnginePath：{model.ViewEnginePath} RelativePath：{model.RelativePath}");

            var selectorsCount = model.Selectors.Count;
            for (var i = 0; i < selectorsCount; ++i)
            {
                var attributeRouteModel = model.Selectors[i].AttributeRouteModel;
                //添加之前
                log.AppendLine($"Template：{attributeRouteModel.Template}");

                if (string.IsNullOrEmpty(attributeRouteModel.Template))
                {
                    continue;
                }
                //该规则是否禁止链接的生成，默认为生成(支持TagHelpers) asp-page="/Index" 
                //https://blog.hueifengcdn.com/uploads/img-177bf172-dc76-42d2-b64c-62c28a058451.png 
                //鼠标箭头放到Home上面，在下面可以显示出来为我们生成的路径，这个路由则是根据我们设置的规则而生成出来的.
                attributeRouteModel.SuppressLinkGeneration = true;
                //添加新的路由模板
                model.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel
                    {
                        //Order 路由匹配顺序
                        //演示一个所有的路由规则都为禁止生成,看下图可以看出，当我们把所有的规则都设置为禁止生成后，我们当鼠标剪头再次放到Home上面时已经不会为我们再生成新的链接了
                        // https://blog.hueifengcdn.com/uploads/img-f80ebee8-c45d-4481-9f9d-3bfe790b2a0c.png
                        //SuppressLinkGeneration = true,
                        Template = $"{attributeRouteModel.Template}.html",
                    }
                });
            }
            //添加完后
            log.AppendLine($"Count：{model.Selectors.Count} ");
            foreach (var item in model.Selectors)
            {
                log.AppendLine($"Template：{item.AttributeRouteModel.Template} ");
            }
            _logger.LogInformation(log.ToString());
        }
    }
}
