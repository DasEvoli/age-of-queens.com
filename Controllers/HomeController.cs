using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Ageofqueenscom.Code;
using Ageofqueenscom.Models;

namespace Ageofqueenscom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger = null;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            try
            {
                model.BlogpostList = Csv.LoadBlogposts();
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                model.BlogpostList = null;
            }
            return View(model);
        }
    }
}
