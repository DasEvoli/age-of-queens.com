using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ageofqueenscom.Models;
using Ageofqueenscom.Code;
using Ageofqueenscom.Entities;
using System.Collections.Generic;
using Ageofqueenscom.Data;
using System.Linq;

namespace Ageofqueenscom.Controllers
{
    public class ModsController : Controller
    {
        private readonly ILogger _logger = null;
        private DataContext _dataContext;
        private ModsViewModel _model;

        public ModsController(ILogger<ModsController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
            _model = new ModsViewModel();
        }

        public IActionResult Index()
        {
            try
            {
                List<ModEntry> mod_entries = _dataContext.ModEntries.ToList();

                foreach(ModEntry e in mod_entries)
                {
                    ModsViewModel.Mod mod = new ModsViewModel.Mod();
                    mod.ModId = e.ModId;
                    mod.Name = e.Name;
                    mod.Description = e.Description;
                    mod.Creator = e.Creator;
                    mod.UploadDate = e.UploadDate;
                    mod.Category = e.Category;
                    mod.ImageUrl = e.ImageUrl;
                    mod.ModUrl = "https://www.ageofempires.com/mods/details/" + e.ModId.ToString();
                    
                    _model.ModList.Add(mod);
                }
                // Shuffling list so introductions are randomly displayed
                var rnd = new Random();
                _model.ModList = _model.ModList.OrderBy(x => rnd.Next()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return View(_model);
        }
    }
}
