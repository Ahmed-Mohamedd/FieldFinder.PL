using Microsoft.AspNetCore.Mvc;

namespace FieldFinder.PL.Areas.Admin.Controllers
{
    public class CoachController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
