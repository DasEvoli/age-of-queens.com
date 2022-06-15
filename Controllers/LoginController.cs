using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Ageofqueenscom.Code;
using Ageofqueenscom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Ageofqueenscom.Data;
using System.Threading.Tasks;
using System.Linq;
using Ageofqueenscom.Entities;
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

		public async Task<IActionResult> Validate(IFormCollection form)
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
                return RedirectToAction("index", "home");
            }
            else
            {
                model.Error = true;
                model.ErrorMessage = "Was not able to login.";
            }
            return View("Index", model);
		}

        public IActionResult Error()
        {
            return View();
        }
    }
}
