using IdealUmbrella.TradeMatrix.Models.Enums;

namespace IdealUmbrella.TradeMatrix.Models
{


    public class TradeAssessmentMatrix
    {
        public TradeAssessmentMatrix(
            string title,
            string buyerName,
            string sellerName,
            string distributorName,
            string goodHsCode)
        {
            Title = title;
            BuyerName = buyerName;
            SellerName = sellerName;
            DistributorName = distributorName;
            HsCode = goodHsCode;
            HsCodeDescription = $"Glass Bottles, and capping items";
            MatrixUniqueId = Guid.NewGuid();
            GeneratedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// A title for the Trade Assessment Matrix
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// A human-readable name for the buyer of the good
        /// </summary>
        public string BuyerName { get; }

        /// <summary>
        /// A human-readable name for the seller of the good
        /// </summary>
        public string SellerName { get; }

        /// <summary>
        /// A human-readable name for the distributor of the good
        /// </summary>
        public string DistributorName { get; }

        /// <summary>
        /// The HSCode of the good
        /// </summary>
        public string HsCode { get; }
        public string HsCodeDescription { get; }

        /// <summary>
        /// A unique ID for this matrix
        /// </summary>
        public Guid MatrixUniqueId { get; }

        /// <summary>
        /// The datetime this matrix was generated in UTC
        /// </summary>
        public DateTime GeneratedDate { get; }
        public List<TradeAssessmentMatrixCell> Grades { get; set; }

    }

    public class TradeAssessmentMatrixCell
    {
        public TradeAssessmentMatrixComponent Component { get; set; }
        public TradeAssessmentMatrixDimension Dimension { get; set; }
        public TradeAssessmentMatrixGrade Grade { get; set; }
    }
}
