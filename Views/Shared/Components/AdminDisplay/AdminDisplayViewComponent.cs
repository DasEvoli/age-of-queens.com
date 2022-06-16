using System;
using System.Linq;
using Ageofqueenscom.Data;
using Microsoft.AspNetCore.Mvc;
using ageofqueenscom.code;

namespace ageofqueenscom.ViewComponents
{
	public class AdminDisplayViewComponent : ViewComponent
	{
		private DataContext _dataContext;
		public AdminDisplayViewComponent(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public IViewComponentResult Invoke()
		{

			if(Helpers.isAdmin(Request.Cookies["session_id"], Request.Cookies["username"], _dataContext))
			{
				return View("Default");
			}

			return Content(String.Empty);
		}
	}
}