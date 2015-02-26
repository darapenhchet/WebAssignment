using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment.Models;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            ViewModel mymodel = new ViewModel();
            mymodel.Users = AccountDAO.ListAllUsers();
            mymodel.Posts = ArticleDAO.List();
            return View(mymodel);
        }



    }
}
