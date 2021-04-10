using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ViewComponentDemo.ViewComponents
{
    [ViewComponent(Name ="Button")]
    public class ButtonTest : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ButtonType type = ButtonType.Success)
        {
            ViewBag.Type = type;
            return View();
        }
    }

    public enum ButtonType
    {
        Default,
        Primary,
        Success,
        Info,
        Warning,
        Danger,
        Link
    }
}
