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
                    ModsViewModel.Mod mod = new ModsViewModel.Mod
                    {
                        ModId = e.ModId,
                        Name = e.Name,
                        Description = e.Description,
                        Creator = e.Creator,
                        UploadDate = e.UploadDate,
                        Category = e.Category,
                        ImageUrl = e.ImageUrl,
                        ModUrl = "https://www.ageofempires.com/mods/details/" + e.ModId.ToString()
                    };
                    _model.ModList.Add(mod);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return View(_model);
        }
    }
}
