using System;
using ageofqueenscom.Code;
using ageofqueenscom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ageofqueenscom.Controllers
{
    public class ActiveEventController : Controller
    {
        private readonly ILogger _logger = null;
        public ActiveEventController(ILogger<ActiveEventController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ActiveEventViewModel model = new ActiveEventViewModel();
            try
            {
                model = Csv.LoadActiveEvent();
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            
            return View(model);
        }
    }
}
