using Microsoft.AspNetCore.Mvc;

namespace Hospital.Areas.Admin.Controllers
{
    public class HospitalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
