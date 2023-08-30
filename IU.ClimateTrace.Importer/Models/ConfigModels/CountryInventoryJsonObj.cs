using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IU.ClimateTrace.Importer.Models.ConfigModels
{
    public class CountryInventoryJsonObj
    {
        public Countrylist[] CountryList { get; set; }
    }
    public class Countrylist
    {
        public string Name { get; set; }
        public string Alpha3 { get; set; }
        public string CountryCode { get; set; }
    }

    
}
