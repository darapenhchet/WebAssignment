using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;
using Assignment.Areas.Admin.Models;
using System.Net;

namespace Assignment.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View(); 
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult SignUp(InsertUser user)
        {
            if (ModelState.IsValid && AccountDAO.CreateUser(user))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(ViewModel model, string returnUrl)
        {
            if (ModelState.IsValid && AccountDAO.SignIn(model.SignIn))
            {
                FormsAuthentication.SetAuthCookie(model.SignIn.Username, model.SignIn.Remember);
                if (returnUrl == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(returnUrl);
            }
            ModelState.AddModelError("", "Sign is failure. Please check username and password again.");
            return View();
        }

        public ActionResult SignOut()
        {
            AccountDAO.SignOut();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult UpdateProfile()
        {
            return View(AccountDAO.DetailUser(AccountDAO.Id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(UpdateUser user)
        {
            if (ModelState.IsValid && AccountDAO.UpdateUser(user))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult DetailProfile()
        {
            return View(AccountDAO.DetailUser(AccountDAO.Id));
        }

        [HttpGet]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DetailUser(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(AccountDAO.DetailUser(id));
        }

        [HttpGet]
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword p)
        {
            if (ModelState.IsValid && AccountDAO.ChangePassword(p))
            {
                return RedirectToAction("Index", "Home");
            }
            return View(p);
        }
    }
}
