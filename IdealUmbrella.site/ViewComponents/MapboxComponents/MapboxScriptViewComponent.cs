using IdealUmbrella.site.Models.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IdealUmbrella.site.ViewComponents.MapboxComponents
{
    public class MapboxScriptViewComponent : ViewComponent
    {
        private readonly IOptions<MapboxConfig> _mapboxSettings;
        public MapboxScriptViewComponent(IOptions<MapboxConfig> mapboxSettings)
        {
            _mapboxSettings = mapboxSettings;
        }

        /// <summary>
        /// Renders the mapbox html key to the front-end
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            MapboxViewModel mapConfigWithKey = new MapboxViewModel
            {
			    FrontendKey = _mapboxSettings.Value.Settings.FrontEndKey
			};

            return View("MapboxView_HeaderScripts",
                        mapConfigWithKey);
        }

    }
}
