using Microsoft.AspNetCore.Mvc;
using ageofqueenscom.Models;
using ageofqueenscom.Code;

namespace ageofqueenscom.Controllers
{
    public class ModsController : Controller
    {
        public IActionResult Index()
        {
            ModsViewModel model = new ModsViewModel();
            model.ModList = Csv.LoadMods();
            return View(model);
        }
    }
}
