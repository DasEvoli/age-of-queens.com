using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Ageofqueenscom.Data;
using Ageofqueenscom.Models;
using System.Linq;
using Ageofqueenscom.Entities;
using System.Collections.Generic;
using ageofqueenscom.code;
using System.Collections.Specialized;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Ageofqueenscom.Controllers
{
    public class AdminConsoleController : Controller
    {
        private readonly ILogger _logger;
        private DataContext _dataContext;
        private HomeViewModel _model;

        public AdminConsoleController(ILogger<HomeController> logger, DataContext dataContext)
        {
            _dataContext = dataContext;
            _logger = logger;
            _model = new HomeViewModel();
        }
        public IActionResult Index()
        {
            try
            {
                if(Helpers.isAdmin(Request.Cookies["session_id"], Request.Cookies["username"], _dataContext))
                {
                    return View(_model);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return View(_model);
        }


        [HttpPost]
        public IActionResult UpdateBlog()
        {
            if(!Helpers.isAdmin(Request.Cookies["session_id"], Request.Cookies["username"], _dataContext)) return Unauthorized();
            try
            {
                string headline = Request.Form["headline"];
                string content = Request.Form["content"];
                string author = Request.Form["author"];
                if(String.IsNullOrEmpty(headline) 
                || String.IsNullOrEmpty(content) 
                || String.IsNullOrEmpty(author) 
                || Request.Form.Files.Count == 0)
                {
                    throw new Exception("Not every required data was given.");
                }
                IFormFile file = Request.Form.Files[0];


                string imageString = Helpers.GetRandomString(12);
                if(file.ContentType == "image/png")
                {
                    imageString += ".png";
                }
                else if(file.ContentType == "image/jpeg")
                {
                    imageString += ".jpg";
                }
                else throw new Exception("No correct file type specified.");

                using FileStream fs = new FileStream($"./wwwroot/images/blog/{imageString}", FileMode.Create);
                file.CopyTo(fs);

                BlogEntry entry = new BlogEntry();
                entry.Headline = headline;
                entry.Content = content;
                entry.ImageName = imageString;
                entry.Author = author;
                entry.CreatedAt = DateTime.Now;

                _dataContext.Add(entry);
                _dataContext.SaveChanges();

                return Ok();
                
            }
            catch(Exception e)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult UpdateIntroduction()
        {
            if(!Helpers.isAdmin(Request.Cookies["session_id"], Request.Cookies["username"], _dataContext)) return Unauthorized();
            try
            {
                string name = Request.Form["name"];
                string description = Request.Form["description"];
                string twitter = Request.Form["twitter"];
                string youtube = Request.Form["youtube"];
                string twitch = Request.Form["twitch"];
                string instagram = Request.Form["instagram"];
                if(String.IsNullOrEmpty(name) 
                || String.IsNullOrEmpty(description) 
                || Request.Form.Files.Count == 0)
                {
                    throw new Exception("Not every required data was given.");
                }
                IFormFile file = Request.Form.Files[0];
                string imageString = Helpers.GetRandomString(12);
                if(file.ContentType == "image/png")
                {
                    imageString += ".png";
                }
                else if(file.ContentType == "image/jpeg")
                {
                    imageString += ".jpg";
                }
                else throw new Exception("No correct file type specified.");
                using FileStream fs = new FileStream($"./wwwroot/images/introduction/{imageString}", FileMode.Create);
                file.CopyTo(fs);

                IntroductionEntry entry = new IntroductionEntry();
                entry.Name = name;
                entry.Description = description;
                entry.ImageUrl = imageString;
                entry.TwitterUrl = twitter;
                entry.YoutubeUrl = youtube;
                entry.TwitchUrl = twitch;
                entry.InstagramUrl = instagram;

                _dataContext.Add(entry);
                _dataContext.SaveChanges();

                return Ok();
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest();
            }

        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
