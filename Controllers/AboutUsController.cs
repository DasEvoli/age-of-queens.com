using System;
using Microsoft.AspNetCore.Mvc;
using ageofqueenscom.Models;
using Microsoft.Extensions.Logging;

namespace ageofqueenscom.Controllers
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
            try
            {
                model.Introductions = Code.Csv.LoadIntroductions();
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return View(model);
        }

        public IActionResult PastEvents()
        {
            return View();
        }
    }
}
