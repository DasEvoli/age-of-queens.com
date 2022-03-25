using Microsoft.AspNetCore.Mvc;
using ageofqueenscom.Models;

namespace ageofqueenscom.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            // Default View
            return View("WhatIsAgeofqueens");
        }

        public IActionResult WhatIsAgeofqueens()
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
