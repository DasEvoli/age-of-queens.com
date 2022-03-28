using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ageofqueenscom.Models;
using Ageofqueenscom.Code;

namespace Ageofqueenscom.Controllers
{
    public class ModsController : Controller
    {
        private readonly ILogger _logger = null;
        public ModsController(ILogger<ModsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ModsViewModel model = new ModsViewModel();
            try
            {
                model.ModList = Csv.LoadMods();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                model.ModList = null;
            }
            return View(model);
        }
    }
}
