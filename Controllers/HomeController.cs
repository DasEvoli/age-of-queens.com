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
                List<BlogEntry> blog_entries = _dataContext.BlogEntries.ToList();

                foreach(BlogEntry b in blog_entries)
                {
                    HomeViewModel.Blogpost blogPost = new HomeViewModel.Blogpost();
                    blogPost.Title = b.Headline;
                    blogPost.Content = b.Content;
                    blogPost.ImageName = b.ImageName;
                    blogPost.Author = b.Author;
                    blogPost.Created = b.CreatedAt;
                    
                    _model.BlogpostList.Add(blogPost);
                }
                _model.BlogpostList.Reverse();
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
