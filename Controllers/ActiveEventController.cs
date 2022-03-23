using ageofqueenscom.Code;
using ageofqueenscom.Models;
using Microsoft.AspNetCore.Mvc;

namespace ageofqueenscom.Controllers
{
    public class ActiveEventController : Controller
    {
        public IActionResult Index()
        {

            ActiveEventViewModel model = new ActiveEventViewModel();
            model = Csv.InitializeActiveEvent();

            return View(model);
        }
    }
}
