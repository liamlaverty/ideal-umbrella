using IdealUmbrella.TradeMatrix.Models;
using IdealUmbrella.TradeMatrix.Models.Enums;

namespace IdealUmbrella.site.Models.ViewModels.SustainableTradeGenerator
{
    public class TradeAssessmentMatrixViewModel : TradeMatrixGenerateRequestDto
    {
        public TradeAssessmentMatrixViewModel(string title, 
            string buyerName,
            string sellerName,
            string distributorName,
            string goodHsCode,
            TradeAssessmentMatrixGrade useOfProceedsEnvironmental,
            TradeAssessmentMatrixGrade useOfProceedsSocioEconomic,
            TradeAssessmentMatrixGrade sellerEnvironmental,
            TradeAssessmentMatrixGrade sellerSocioEconomic,
            TradeAssessmentMatrixGrade buyerEnvironmental,
            TradeAssessmentMatrixGrade buyerSocioEconomic,
            TradeAssessmentMatrixGrade distributionEnvironmental,
            TradeAssessmentMatrixGrade distributionSocioEconomic,
            bool includeSummaryNotes)
        {
            base.Title = title;
            base.BuyerName = buyerName;
            base.SellerName = sellerName;
            base.DistributorNames = distributorName;
            base.GoodHsCode = goodHsCode;

            base.UseOfProceedsEnvironmental = useOfProceedsEnvironmental;
            base.UseOfProceedsSocioEconomic = useOfProceedsSocioEconomic;

            base.SellerEnvironmental = sellerEnvironmental;
            base.SellerSocioEconomic = sellerSocioEconomic;

            base.BuyerEnvironmental = buyerEnvironmental;
            base.BuyerSocioEconomic = buyerSocioEconomic;

            base.DistributionEnvironmental = distributionEnvironmental;
            base.DistributionSocioEconomic = distributionSocioEconomic;

            this.IncludeSummaryNotes = includeSummaryNotes;

            this.HsCodeDescription = "Glass bottles, and capping items";

            this.AssessmentDate = DateTime.UtcNow;
            this.AssessmentID = Guid.NewGuid();
        }

        public bool IncludeSummaryNotes { get;  }
        public string HsCodeDescription { get; private set; }
        public DateTime AssessmentDate { get; }
        public Guid AssessmentID { get; }

    }
}
