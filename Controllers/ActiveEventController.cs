using ageofqueenscom.Code;
using ageofqueenscom.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


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
