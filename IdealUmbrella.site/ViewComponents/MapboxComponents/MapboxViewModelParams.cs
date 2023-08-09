namespace IdealUmbrella.site.ViewComponents.MapboxComponents
{

    /// <summary>
    /// Params to pass to the Mapbox View Model
    /// </summary>
    public class MapboxViewModelParams : MapboxViewModel
    {
        /// <summary>
        /// The HTML section of the mapbox code to render 
        /// 
        /// Will toggle between:
        ///     - <see cref="MapboxViewSection.BodyMap"/> (the <div> element containing actual map as displayed on the front-end)
        ///     - <see cref="MapboxViewSection.FooterScripts"/>  (the <script> element, containing the map's settings)
        /// </summary>
        public MapboxViewSection Section { get; set; }        
    }

    public enum MapboxViewSection
    {
        BodyMap,
        FooterScripts,
    }
}
