using System;
using ageofqueenscom.Code;
using ageofqueenscom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ageofqueenscom.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly ILogger _logger = null;
        public LeaderboardController(ILogger<LeaderboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            LeaderboardViewModel model = new LeaderboardViewModel();
            try
            {
                model.LeaderboardPlayerListRM = Csv.LoadLeaderboardRM();
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                model.LeaderboardPlayerListRM = null;
            }
            
            return View(model);
        }
    }
}
