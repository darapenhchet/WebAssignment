﻿using System;
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
                if (Account.CreateUser(user))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "SignUp is failure");
            return View();
        }

        [HttpGet]
        public ActionResult SignIn() {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(CheckUser user) {
            if (ModelState.IsValid) {
                if (Account.SignIn(user))
                {
                    FormsAuthentication.SetAuthCookie(user.Username, user.Remember);
                    return RedirectToAction("Index", "Admin");
                }
            }
            ModelState.AddModelError("", "SignIn data is incorrect!");
            return View();
        }

        public ActionResult SignOut() {
            Account.SignOut();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            if (Request.IsAuthenticated)
            {
                User user = Account.DetailUser(Account.getId());
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
                if (Account.UpdateUser(user))
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
                User user = Account.DetailUser(id);
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
                if (Account.UpdateUser(user))
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
                return View(Account.DetailUser(Account.getId()));
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
                return View(Account.DetailUser(id));
            }
            return RedirectToAction("SignIn", "Admin");  
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            if (Request.IsAuthenticated)
            {
                User user = Account.DetailUser(id);
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
                if (Account.DeleteUser(user.Id))
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
                if (Account.ChangePassword(p))
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
                return View(Account.ListAllUsers());
            }
            return RedirectToAction("SignIn", "Admin"); 
        }

    }
}