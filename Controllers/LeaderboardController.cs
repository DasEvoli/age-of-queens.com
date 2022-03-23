using ageofqueenscom.Code;
using ageofqueenscom.Models;
using Microsoft.AspNetCore.Mvc;

namespace ageofqueenscom.Controllers
{
    public class LeaderboardController : Controller
    {
        public IActionResult Index()
        {
            LeaderboardViewModel model = new LeaderboardViewModel();
            model.LeaderboardPlayerListRM = Csv.LoadLeaderboardRM();
            

            return View(model);
        }
    }
}
