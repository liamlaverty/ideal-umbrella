using IdealUmbrella.site.Models.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IdealUmbrella.site.ViewComponents
{
    public class MapboxViewComponent : ViewComponent
    {
        private readonly IOptions<MapboxConfig> _mapboxSettings;
        public MapboxViewComponent(IOptions<MapboxConfig> mapboxSettings)
        {
            _mapboxSettings = mapboxSettings;
        }
        public IViewComponentResult Invoke()
        {
            return View(new MapboxViewComponentDto
            {
                FrontendKey = _mapboxSettings.Value.Settings.FrontEndKey
            });
        }
    }

    public class MapboxViewComponentDto
    {
        public string FrontendKey { get; set; }
    }
}
