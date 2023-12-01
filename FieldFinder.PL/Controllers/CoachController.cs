using Microsoft.AspNetCore.Mvc;

namespace FieldFinder.PL.Controllers
{
    public class CoachController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
