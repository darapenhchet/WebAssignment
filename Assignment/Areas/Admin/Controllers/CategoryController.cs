using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        

    }
}
