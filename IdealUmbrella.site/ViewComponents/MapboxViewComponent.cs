using Microsoft.AspNetCore.Mvc;

namespace IdealUmbrella.site.ViewComponents
{
    public class MapboxViewComponent : ViewComponent
    {
        public MapboxViewComponent()
        {
            
        }
        public IViewComponentResult Invoke()
        {
            return View(new MapboxViewComponentDto
            {
                FrontendKey = ""
            });
        }
    }

    public class MapboxViewComponentDto
    {
        public string FrontendKey { get; set; }
    }
}
