using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Ageofqueenscom.Data;
using Ageofqueenscom.Models;
using System.Linq;
using Ageofqueenscom.Entities;
using System.Collections.Generic;
using ageofqueenscom.code;

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
        public IActionResult Error()
        {
            return View();
        }
    }
}
