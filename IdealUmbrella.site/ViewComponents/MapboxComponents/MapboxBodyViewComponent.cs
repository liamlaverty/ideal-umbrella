using Microsoft.AspNetCore.Mvc;

namespace IdealUmbrella.site.ViewComponents.MapboxComponents
{

    public class MapboxBodyViewComponent : ViewComponent
    {
        public MapboxBodyViewComponent()
        {
        }


        /// <summary>
        /// renders the mapbox map's <div></div> elements
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
            MapboxViewModel mapConfig = modelParams;
            return View("MapboxView_BodyMap", mapConfig);
        }
    }
}
