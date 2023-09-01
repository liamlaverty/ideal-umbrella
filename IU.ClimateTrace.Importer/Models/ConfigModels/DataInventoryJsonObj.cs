namespace IU.ClimateTrace.Importer.Models.ConfigModels
{
    /// <summary>
    /// Matches the file at:
    /// 
    /// `data-inventories.dev.json`
    /// </summary>
    public class DataInventoryJsonObj
    {
        public Datainventory[] DataInventories { get; set; }
    }
    public class Datainventory
    {
        public Inventory[] Inventories { get; set; }
        public string Directory { get; set; }
    }

    public class Inventory
    {
        public string FileName { get; set; }
        public string[] CsvColumns { get; set; }
    }
}
