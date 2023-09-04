using Microsoft.AspNetCore.Mvc;

namespace IU.ClimateTrace.Importer.Web.API.Controllers
{
    public class CorporateEmissionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
