using IdealUmbrella.site.Models.ViewModels.SustainableTradeGenerator;
using IdealUmbrella.TradeMatrix.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace IdealUmbrella.site.ViewComponents.TradeAssessmentMatrixComponents
{
    public class TradeAssessmentMatrixViewComponent : ViewComponent
    {
        /// <summary>
        ///  Renders a full TradeAssessment matrix based on a TradeAssessmentMtxViewModel
        /// </summary>
        /// <param name="mtxViewModel"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(TradeAssessmentMatrixViewModel mtxViewModel)
        {
            return View("TradeGradeMatrix", mtxViewModel);
        }
    }


    public class TradeAssessmentMatrixGradeViewComponent : ViewComponent
    {
        /// <summary>
        /// Renders a single TradeMatrix branded A/B/N/U
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(TradeAssessmentMatrixGrade grade)
        {
            return View("TradeGradeSpan", grade.ToString());
        }
    }


    
}
