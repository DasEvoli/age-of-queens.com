using ageofqueenscom.Code;
using ageofqueenscom.Code;
using ageofqueenscom.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace ageofqueenscom.Controllers
{
    public class ActiveEventController : Controller
    {
        public IActionResult Index()
        {

            ActiveEventViewModel model = new ActiveEventViewModel();

            //model.SeedSoloTables = new List<SeedSoloTable>();
            //model.SeedSoloTables.Add(Csv.LoadSeedSoloTable(0));
            //model.SeedSoloTables.Add(Csv.LoadSeedSoloTable(5));

            model.SeedTeamTables = new List<SeedTeamTableModel>();
            model.SeedTeamTables.Add(Csv.LoadSeedTeamTable(1));
            model.RosterTable = Csv.LoadRosterTable();

            return View(model);
        }
    }
}
