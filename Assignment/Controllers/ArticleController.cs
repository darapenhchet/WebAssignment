using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//using System.Dynamic; // for using dynamic model (multiple models in a view)

using Assignment.Models;

namespace Assignment.Controllers
{
    public class ArticleController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ViewModel mymodel = new ViewModel();
                mymodel.Posts = ArticleDAO.List();
                mymodel.Users = AccountDAO.ListAllUsers();
                return View(mymodel);
            }
            return RedirectToAction("SignIn", "Admin");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("SignIn", "Admin");
        }

        [HttpPost]
        public ActionResult Create(SetPost p) {
            if (Request.IsAuthenticated) {
                if (ModelState.IsValid) {
                    if (ArticleDAO.Create(p)) {
                        ViewBag.Msg = "Article was created successfully.";
                        return RedirectToAction("Index", "Article");
                    }
                }
                ModelState.AddModelError("", "Can not create an article.");
                return View();
            }
            return RedirectToAction("SignIn", "Admin");

        }

    }
}
