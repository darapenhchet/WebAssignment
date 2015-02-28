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
    [Authorize]
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(MyModel.getModels());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InsertUser user)
        {
            if (ModelState.IsValid && AccountDAO.CreateUser(user))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(AccountDAO.DetailUser(id));
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetUser user = AccountDAO.DetailUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateUser user)
        {
            if (ModelState.IsValid && AccountDAO.UpdateUser(user))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            GetUser user = AccountDAO.DetailUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteUser user)
        {
            if (ModelState.IsValid && AccountDAO.DeleteUser(user.Id))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
