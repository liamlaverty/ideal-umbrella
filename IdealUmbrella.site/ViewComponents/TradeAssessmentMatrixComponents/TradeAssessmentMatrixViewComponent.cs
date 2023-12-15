using Microsoft.AspNetCore.Mvc;

namespace IdealUmbrella.site.ViewComponents.TradeAssessmentMatrixComponents
{
    public class TradeAssessmentMatrixGradeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string grade)
        {
            return View("TradeGradeSpan", grade);
        }
    }


    public class TradeAssessmentMatrixViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string grade)
        {
            return View("TradeGradeMatrix", grade);
        }
    }
}
