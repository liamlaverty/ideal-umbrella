using IdealUmbrella.TradeMatrix.Models;
using IdealUmbrella.TradeMatrix.Models.Enums;
using IdealUmbrella.TradeMatrix.Services.Interface;

namespace IdealUmbrella.TradeMatrix.Services.Impl
{
    public class TradeMatrixService : ITradeMatrixService
    {
        public TradeAssessmentMatrix Generate(TradeMatrixGenerateRequestDto data)
        {
            var result = new TradeAssessmentMatrix(
                title: data.Title,
                buyerName: data.BuyerName,
                sellerName: data.SellerName,
                distributorName: data.DistributorNames,
                goodHsCode: data.GoodHsCode
                );

            result.Grades = new List<TradeAssessmentMatrixCell>
            {
                new TradeAssessmentMatrixCell
                {
                    Component = TradeAssessmentMatrixComponent.Environmental,
                    Dimension = TradeAssessmentMatrixDimension.UseOfProceeds,
                    Grade = data.UseOfProceedsEnvironmental
                },
                new TradeAssessmentMatrixCell
                {
                    Component = TradeAssessmentMatrixComponent.SocioEconomic,
                    Dimension = TradeAssessmentMatrixDimension.UseOfProceeds,
                    Grade = data.UseOfProceedsSocioEconomic
                },
                new TradeAssessmentMatrixCell
                {
                    Component = TradeAssessmentMatrixComponent.Environmental,
                    Dimension = TradeAssessmentMatrixDimension.Seller,
                    Grade = data.SellerEnvironmental
                },
                new TradeAssessmentMatrixCell
                {
                    Component = TradeAssessmentMatrixComponent.SocioEconomic,
                    Dimension = TradeAssessmentMatrixDimension.Seller,
                    Grade = data.SellerSocioEconomic
                },
                new TradeAssessmentMatrixCell
                {
                    Component = TradeAssessmentMatrixComponent.Environmental,
                    Dimension = TradeAssessmentMatrixDimension.Buyer,
                    Grade = data.BuyerSocioEconomic
                },
                new TradeAssessmentMatrixCell
                {
                    Component = TradeAssessmentMatrixComponent.SocioEconomic,
                    Dimension = TradeAssessmentMatrixDimension.Buyer,
                    Grade = data.BuyerEnvironmental
                },
                new TradeAssessmentMatrixCell
                {
                    Component = TradeAssessmentMatrixComponent.Environmental,
                    Dimension = TradeAssessmentMatrixDimension.Distribution,
                    Grade = data.DistributionEnvironmental
                },
                new TradeAssessmentMatrixCell
                {
                    Component = TradeAssessmentMatrixComponent.SocioEconomic,
                    Dimension = TradeAssessmentMatrixDimension.Distribution,
                    Grade = data.DistributionSocioEconomic
                },
                new TradeAssessmentMatrixCell
                {
                    Component = TradeAssessmentMatrixComponent.Environmental,
                    Dimension = TradeAssessmentMatrixDimension.Overall,
                    Grade = TradeAssessmentMatrixGrade.U
                },
                new TradeAssessmentMatrixCell
                {
                    Component = TradeAssessmentMatrixComponent.SocioEconomic,
                    Dimension = TradeAssessmentMatrixDimension.Overall,
                    Grade = TradeAssessmentMatrixGrade.U
                },
            };

            return result;
        }

        public TradeAssessmentMatrix Get()
        {
            return new TradeAssessmentMatrix(
                title: "Example trade assessment",
                buyerName: "Example Buyer",
                sellerName: "Example seller",
                distributorName: "Example distributor",
                goodHsCode: "702000"
                )
            {
                Grades = new List<TradeAssessmentMatrixCell>
                {
                    new TradeAssessmentMatrixCell
                    {
                        Component = TradeAssessmentMatrixComponent.Environmental,
                        Dimension = TradeAssessmentMatrixDimension.UseOfProceeds,
                        Grade = TradeAssessmentMatrixGrade.U
                    },
                    new TradeAssessmentMatrixCell
                    {
                        Component = TradeAssessmentMatrixComponent.SocioEconomic,
                        Dimension = TradeAssessmentMatrixDimension.UseOfProceeds,
                        Grade = TradeAssessmentMatrixGrade.U
                    },
                    new TradeAssessmentMatrixCell
                    {
                        Component = TradeAssessmentMatrixComponent.Environmental,
                        Dimension = TradeAssessmentMatrixDimension.Seller,
                        Grade = TradeAssessmentMatrixGrade.U
                    },
                    new TradeAssessmentMatrixCell
                    {
                        Component = TradeAssessmentMatrixComponent.SocioEconomic,
                        Dimension = TradeAssessmentMatrixDimension.Seller,
                        Grade = TradeAssessmentMatrixGrade.U
                    },
                    new TradeAssessmentMatrixCell
                    {
                        Component = TradeAssessmentMatrixComponent.Environmental,
                        Dimension = TradeAssessmentMatrixDimension.Buyer,
                        Grade = TradeAssessmentMatrixGrade.U
                    },
                    new TradeAssessmentMatrixCell
                    {
                        Component = TradeAssessmentMatrixComponent.SocioEconomic,
                        Dimension = TradeAssessmentMatrixDimension.Buyer,
                        Grade = TradeAssessmentMatrixGrade.U
                    },
                    new TradeAssessmentMatrixCell
                    {
                        Component = TradeAssessmentMatrixComponent.Environmental,
                        Dimension = TradeAssessmentMatrixDimension.Distribution,
                        Grade = TradeAssessmentMatrixGrade.U
                    },
                    new TradeAssessmentMatrixCell
                    {
                        Component = TradeAssessmentMatrixComponent.SocioEconomic,
                        Dimension = TradeAssessmentMatrixDimension.Distribution,
                        Grade = TradeAssessmentMatrixGrade.U
                    },
                    new TradeAssessmentMatrixCell
                    {
                        Component = TradeAssessmentMatrixComponent.Environmental,
                        Dimension = TradeAssessmentMatrixDimension.Overall,
                        Grade = TradeAssessmentMatrixGrade.U
                    },
                    new TradeAssessmentMatrixCell
                    {
                        Component = TradeAssessmentMatrixComponent.SocioEconomic,
                        Dimension = TradeAssessmentMatrixDimension.Overall,
                        Grade = TradeAssessmentMatrixGrade.U
                    },
                }
            };
        }
    }
}
