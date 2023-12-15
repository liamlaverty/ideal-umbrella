using Microsoft.AspNetCore.Mvc;

namespace IdealUmbrella.site.ViewComponents.TradeAssessmentMatrixComponents
{

    /// <summary>
    /// Renders a full TradeMatrix
    /// </summary>
    public class TradeAssessmentMatrixViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string grade)
        {
            return View("TradeGradeMatrix", grade);
        }
    }

    /// <summary>
    ///  Renders a TradeMatrix branded A/B/N/U
    /// </summary>
    public class TradeAssessmentMatrixGradeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string grade)
        {
            return View("TradeGradeSpan", grade);
        }
    }


    
}
