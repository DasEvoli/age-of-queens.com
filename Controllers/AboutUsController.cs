using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ageofqueenscom.Models;
using Ageofqueenscom.Data;
using System.Collections.Generic;
using Ageofqueenscom.Entities;
using System.Linq;

namespace Ageofqueenscom.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ILogger _logger = null;
        private DataContext _dataContext;
        private IntroductionsViewModel _model;


        public AboutUsController(ILogger<AboutUsController> logger, DataContext dataContext)
        {
            _dataContext = dataContext;
            _logger = logger;
            _model = new IntroductionsViewModel();
        }
        public IActionResult Index()
        {
            return View("WhatIsAgeofqueens");   // Default View.
        }

        public IActionResult WhatIsAgeofqueens()
        {
            return View();
        }

        public IActionResult Introductions()
        {
            try
            {
                List<IntroductionEntry> introduction_entries = _dataContext.IntroductionEntries.ToList();
                
                foreach(IntroductionEntry b in introduction_entries)
                {
                    IntroductionsViewModel.Introduction introduction = new IntroductionsViewModel.Introduction
                    {
                        Name = b.Name,
                        Content = b.Content,
                        ImageUrl = b.ImageUrl,
                        TwitterUrl = b.TwitterUrl,
                        YoutubeUrl = b.YoutubeUrl,
                        TwitchUrl = b.TwitchUrl,
                        InstagramUrl = b.InstagramUrl
                    };
                    _model.IntroductionList.Add(introduction);
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return View(_model);
        }

        public IActionResult PastEvents()
        {
            return View();
        }
    }
}
