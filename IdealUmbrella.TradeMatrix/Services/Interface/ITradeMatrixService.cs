using IdealUmbrella.TradeMatrix.Models;

namespace IdealUmbrella.TradeMatrix.Services.Interface
{
    public interface ITradeMatrixService
    {
        TradeAssessmentMatrix Get();
        public TradeAssessmentMatrix Generate(TradeMatrixGenerateRequestDto data);

    }
}
