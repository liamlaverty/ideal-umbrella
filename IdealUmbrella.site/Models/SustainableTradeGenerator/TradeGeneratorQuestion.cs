using System.Text.RegularExpressions;

namespace IdealUmbrella.site.Models.SustainableTradeGenerator
{
    public class TradeGeneratorQuestion
    {
        public string Title{ get; set; }
        public string Dimension { get; set; }

        public string Label { get
            {
                return $"{Dimension} {Title.ToLower()}";
            } }

        /// <summary>
        ///  Gets a html friendly string from the title/dimension
        /// </summary>
        public string HtmlID
        {
            get
            {
                var result = $"{Dimension}_{Title}";
                result = result.ToLower();
                result = Regex.Replace(result, @"[^A-Za-z0-9\s-]", "");
                result = Regex.Replace(result, @"\s+", " ").Trim();
                result = Regex.Replace(result, @"\s", "-");
                return result;
            }
        }
    }
}
