using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ageofqueenscom.Models;

namespace Ageofqueenscom.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ILogger _logger = null;
        public AboutUsController(ILogger<AboutUsController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View("WhatIsAgeofqueens");   // Default View.
        }

        public IActionResult WhatIsAgeofqueens()
        {
            return View();
        }

        public IActionResult Introductions()
        {
            IntroductionsViewModel model = new IntroductionsViewModel();
            try
            {
                model.Introductions = Code.Csv.LoadIntroductions();
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                model.Introductions = null;
            }
            return View(model);
        }

        public IActionResult PastEvents()
        {
            return View();
        }
    }
}
