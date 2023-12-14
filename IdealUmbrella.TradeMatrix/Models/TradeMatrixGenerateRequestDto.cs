using IdealUmbrella.TradeMatrix.Models.Enums;
using Newtonsoft.Json;

namespace IdealUmbrella.TradeMatrix.Models
{
    /// <summary>
    /// 
    /// {
    ///    title: 'Glass Bottle Trade',
    ///    goodHsCode: '702000',
    ///    sellerName: 'Glass Bottle Corp',
    ///    buyerName: 'International Cola Industries',
    ///    distributorNames: 'example',
    /// 
    ///    useOfProceedsEnvironmental: 'n',
    ///    useOfProceedsSocioEconomic: 'n',
    ///
    ///    sellerEnvironmental: 'n',
    ///    sellerEconomic: 'n',
    ///
    ///    buyerEnvironmental: 'n',
    ///    buyerEconomic: 'n',
    ///
    ///    distributionEnvironmental: 'n',
    ///    distributionEconomic: 'n',
    /// }
    /// 
    /// </summary>
    public class TradeMatrixGenerateRequestDto
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "goodHsCode")]
        public string GoodHsCode { get; set; }


        [JsonProperty(PropertyName = "sellerName")]
        public string SellerName { get; set; }

        [JsonProperty(PropertyName = "buyerName")]
        public string BuyerName { get; set; }

        [JsonProperty(PropertyName = "distributorNames")]
        public string DistributorNames { get; set; }

        [JsonProperty(PropertyName = "useOfProceedsEnvironmental")]
        public TradeAssessmentMatrixGrade UseOfProceedsEnvironmental { get; set; }

        [JsonProperty(PropertyName = "useOfProceedsSocioEconomic")]
        public TradeAssessmentMatrixGrade UseOfProceedsSocioEconomic { get; set; }

        [JsonProperty(PropertyName = "sellerEnvironmental")]
        public TradeAssessmentMatrixGrade SellerEnvironmental { get; set; }
        [JsonProperty(PropertyName = "SellerSocioEconomic")]
        public TradeAssessmentMatrixGrade SellerSocioEconomic { get; set; }

        [JsonProperty(PropertyName = "buyerEnvironmental")]
        public TradeAssessmentMatrixGrade BuyerEnvironmental { get; set; }
        [JsonProperty(PropertyName = "buyerSocioEconomic")]
        public TradeAssessmentMatrixGrade BuyerSocioEconomic { get; set; }

        [JsonProperty(PropertyName = "distributionEnvironmental")]
        public TradeAssessmentMatrixGrade DistributionEnvironmental { get; set; }
        [JsonProperty(PropertyName = "distributionSocioEconomic")]
        public TradeAssessmentMatrixGrade DistributionSocioEconomic { get; set; }
    }
}
