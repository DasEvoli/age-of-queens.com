using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Ageofqueenscom.Data;
using Ageofqueenscom.Models;
using System.Linq;
using Ageofqueenscom.Entities;
using System.Collections.Generic;

namespace Ageofqueenscom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private DataContext _dataContext;
        private HomeViewModel _model;

        public HomeController(ILogger<HomeController> logger, DataContext dataContext)
        {
            _dataContext = dataContext;
            _logger = logger;
            _model = new HomeViewModel();
        }
        public IActionResult Index()
        {
            try
            {
                List<IntroductionEntry> introduction_entries = _dataContext.IntroductionEntries.ToList();
                
                foreach(IntroductionEntry b in introduction_entries)
                {
                    HomeViewModel.Introduction introduction = new HomeViewModel.Introduction();
                    introduction.Name = b.Name;
                    introduction.Description = b.Description;
                    introduction.ImageUrl = b.ImageUrl;
                    introduction.TwitterUrl = b.TwitterUrl;
                    introduction.YoutubeUrl = b.YoutubeUrl;
                    introduction.TwitchUrl = b.TwitchUrl;
                    introduction.InstagramUrl = b.InstagramUrl;
                    
                    _model.IntroductionList.Add(introduction);
                }
                // Shuffling list so introductions are randomly displayed
                var rnd = new Random();
                _model.IntroductionList = _model.IntroductionList.OrderBy(x => rnd.Next()).ToList();
                
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return View(_model);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
