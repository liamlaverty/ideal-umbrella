using IdealUmbrella.site.Models.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IdealUmbrella.site.ViewComponents.MapboxComponents
{
    public class MapboxViewComponent : ViewComponent
    {
        private readonly IOptions<MapboxConfig> _mapboxSettings;
        public MapboxViewComponent(IOptions<MapboxConfig> mapboxSettings)
        {
            _mapboxSettings = mapboxSettings;
        }


        /// <summary>
        /// renders the mapbox map or mapbox map script
        /// </summary>
        /// <param name="section">
        /// The part of the ViewComponent you'd like to render
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if an invalid <see cref="MapboxViewSection"/> is passed
        /// </exception>
        public IViewComponentResult Invoke(MapboxViewModelParams modelParams)
        {
            switch (modelParams.Section)
            {
                case MapboxViewSection.FooterScripts:
                    return View("MapboxView_HeaderScripts",
                        new MapboxViewModel
                        {
                            FrontendKey = _mapboxSettings.Value.Settings.FrontEndKey,
                            MapId = modelParams.MapId,
                        });
                case MapboxViewSection.BodyMap:
                    return View("MapboxView_BodyMap",
                        new MapboxViewModel
                        {
                            MapId = modelParams.MapId
                        });
                default:
                    throw new ArgumentOutOfRangeException(nameof(modelParams.Section), $"Unsupported section type {modelParams.Section}");
            }   
        }
    }
}
