using Microsoft.AspNetCore.Mvc;

namespace ViewComponentDemo.ViewComponents
{
    public class InputViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Input");
        }
    }
}
