using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

using Assignment.Models;
using System.Net;

namespace Assignment.Controllers
{
    public class AdminController : Controller {

        public ActionResult Index() {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("SignIn", "Admin");
        }

        [HttpGet] 
        public ActionResult SignUp() {
            return View();
        }

        [HttpPost] 
        public ActionResult SignUp(InsertUser user)
        {
            if (ModelState.IsValid) {
                if (AccountDAO.CreateUser(user))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "SignUp is failure");
            return View();
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(SignIn user) {
            if (ModelState.IsValid) {
                if (AccountDAO.SignIn(user))
                {
                    FormsAuthentication.SetAuthCookie(user.Username, user.Remember);
                    return RedirectToAction("Index", "Admin");
                }
            }
            ModelState.AddModelError("", "SignIn data is incorrect!");
            return View();
        }

        public ActionResult SignOut() {
            AccountDAO.SignOut();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            if (Request.IsAuthenticated)
            {
                GetUser user = AccountDAO.DetailUser(AccountDAO.Id);
                return View(user);
            }
            else
            {
                return RedirectToAction("SignIn", "Admin");
            }
        }

        [HttpPost]
        public ActionResult UpdateProfile(UpdateUser user)
        {
            if (ModelState.IsValid)
            {
                if (AccountDAO.UpdateUser(user))
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Update is failure");
            }
            return View();
        }
        
        [HttpGet] 
        public ActionResult UpdateUser(int id = 0)
        {
            if (Request.IsAuthenticated)
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
            return RedirectToAction("SignIn", "Admin");    
        }
      
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(UpdateUser user)
        {
            if (ModelState.IsValid)
            {
                if (AccountDAO.UpdateUser(user))
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Update is failure");
            }
            return View();
        }
        
        [HttpGet] 
        public ActionResult DetailProfile()
        {
            if (Request.IsAuthenticated)
            {
                return View(AccountDAO.DetailUser(AccountDAO.Id));
            }
            return RedirectToAction("SignIn", "Admin");   
        }

        [HttpGet]
        public ActionResult DetailUser(int id)
        {
            if (Request.IsAuthenticated)
            {
                if (id <= 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return View(AccountDAO.DetailUser(id));
            }
            return RedirectToAction("SignIn", "Admin");  
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            if (Request.IsAuthenticated)
            {
                GetUser user = AccountDAO.DetailUser(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            return RedirectToAction("SignIn", "Admin");  
        }

        [HttpPost]
        public ActionResult DeleteUser(DeleteUser user)
        {
            if (ModelState.IsValid)
            {
                if (AccountDAO.DeleteUser(user.Id))
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Update is failure");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("SignIn", "Admin"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword p)
        {
            if (ModelState.IsValid)
            {
                if (AccountDAO.ChangePassword(p))
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            ModelState.AddModelError("", "Change password is failure");
            return View(p);
        }

        [HttpGet]
        public ActionResult ListUsers()
        {
            if (Request.IsAuthenticated)
            {
                return View(AccountDAO.ListAllUsers());
            }
            return RedirectToAction("SignIn", "Admin"); 
        }

    }
}
