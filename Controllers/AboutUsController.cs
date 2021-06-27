using Microsoft.AspNetCore.Mvc;
using ageofqueenscom.Models;
using ageofqueenscom.Code;

namespace ageofqueenscom.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View("WhatIsAgeofqueens");
        }

        public IActionResult WhatIsAgeofqueens()
        {
            return View();
        }

        public IActionResult Rules()
        {
            return View();
        }

        public IActionResult Introductions()
        {
            IntroductionsViewModel model = new IntroductionsViewModel();
            model.Introductions = Code.Csv.LoadIntroductions();
            return View(model);
        }

        public IActionResult PastEvents()
        {
            return View();
        }
    }
}
