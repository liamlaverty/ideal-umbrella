using IdealUmbrella.site.Models.Shared.Geo;

namespace IdealUmbrella.site.ViewComponents.MapboxComponents
{


    public class MapboxViewModel
    {
        public string FrontendKey { get; set; }

        /// <summary>
        /// A unique ID for this map, this ID will be used by the 
        /// mapbox js scripts to render the map
        /// </summary>
        public Guid MapId { get; set; }


        /// <summary>
        /// The latlong for the map to focus on
        /// </summary>
        public LatLong LatLong { get; set; }

        /// <summary>
        /// The zoom level for the map
        /// 
        /// Apprx settings: 
        /// 
        /// 0	The Earth
        /// 3	A continent
        /// 4	Large islands
        /// 6	Large rivers
        /// 10	Large roads
        /// 15	Buildings
        /// 
        /// </summary>
        /// <seealso cref="https://docs.mapbox.com/help/glossary/zoom-level/"/>
        public int MapZoom { get; set; }
    }
}
