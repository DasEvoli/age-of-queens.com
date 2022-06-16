using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Ageofqueenscom.Models;
using Microsoft.AspNetCore.Http;
using Ageofqueenscom.Data;
using System.Linq;
using ageofqueenscom.code;

namespace Ageofqueenscom.Controllers
{
    public class LoginController : Controller
    {
        private DataContext _dataContext = null;
        private readonly ILogger _logger = null;

        public LoginController(ILogger<LoginController> logger, DataContext dataContext)
        {
            _dataContext = dataContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("session_id");
            return RedirectToAction("index", "home");
        }

		public IActionResult Validate(IFormCollection form)
        {
            try
            {
                LoginViewModel model = new LoginViewModel();
                string username = form["username"];
                string password = form["password"];
                
                var user = _dataContext.Users.FirstOrDefault(s => s.UserName == username && s.UserPassword == password);
                if(user != null)
                {
                    user.Session = Helpers.GetRandomString(24);
                    user.LastLogin = DateTime.Now;
                    _dataContext.Update(user);
                    _dataContext.SaveChanges();

                    CookieOptions cookieOptions = new CookieOptions();
                    cookieOptions.HttpOnly = true;
                    cookieOptions.Expires =  DateTime.Now.AddDays(31);
                    HttpContext.Response.Cookies.Append("session_id", user.Session, cookieOptions);
                    
                    HttpContext.Response.Cookies.Append("username", user.UserName, cookieOptions);
                    
                    return RedirectToAction("index", "home");
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                return Unauthorized();
            }

		}

        public IActionResult Error()
        {
            return View();
        }
    }
}
