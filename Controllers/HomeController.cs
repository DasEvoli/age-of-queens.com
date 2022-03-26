using System;
using ageofqueenscom.Code;
using ageofqueenscom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ageofqueenscom.Controllers
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

        // TODO: Learn more about those properties. Could also be called Decoration. And I think bindings
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
