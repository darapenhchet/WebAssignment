using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;
using Assignment.Areas.Admin.Models;

namespace Assignment.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(MyModel.getModels());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewModel model = new ViewModel();
            model.AllPosts = ArticleDAO.List();
            model.AllCategories = CategoryDAO.List();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModel p, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null && file.ContentLength > 0)
            {
                // Extract only the filename
                var fileName = System.IO.Path.GetFileName(file.FileName);

                // Store the file inside ~/App_Data/Data folder
                var path = System.IO.Path.Combine(Server.MapPath("~/App_Data/Data"), fileName);

                // Upload file
                file.SaveAs(path);

                p.SetPost.ByUser = AccountDAO.Id;
                p.SetPost.Photo = path; // Store file path to database
                if (ArticleDAO.Create(p.SetPost))
                {
                    ViewBag.Msg = "Article was created successfully.";
                    return RedirectToAction("Index", "Article");
                }
            }
            ModelState.AddModelError("", "Can not create an article.");
            return View();
        }

    }
}
