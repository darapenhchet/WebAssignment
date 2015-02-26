using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Assignment.Models;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            ViewModel mymodel = new ViewModel();
            mymodel.Posts = ArticleDAO.List();
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult Index(ViewModel model) 
        {
            if (ModelState.IsValid)
            {
                if (AccountDAO.SignIn(model.SignIn))
                {
                    FormsAuthentication.SetAuthCookie(model.SignIn.Username, model.SignIn.Remember);
                    return RedirectToAction("Index", "Admin");
                }
            }
            ModelState.AddModelError("", "SignIn data is incorrect!");
            return View();
        }
    }
}
