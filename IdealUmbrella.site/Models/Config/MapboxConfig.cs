namespace IdealUmbrella.site.Models.Config
{
    public class MapboxConfig
    {
        public static readonly string ConfigName = "MapboxConfig";
        public MapboxConfigSettings Settings { get; set; }
    }

    public class MapboxConfigSettings
    {
        public string FrontEndKey { get; set; }
    }
}
