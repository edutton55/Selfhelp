using ApplicationLogger;
using BLL;
using DAL;
using HelpConsciousness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpConsciousness.Controllers
{
    public class AdminController : Controller
    {
        public IUserLogic adminLogic = new UserLogic(new UserDAO(new LoggerIO()), new SQLDAO(), new LoggerIO(), new Hashing());
        static UserVM guy = new UserVM();

        // GET: User
        public ActionResult Index() 
        {
            List<UserVM> users = guy.Map(adminLogic.GetUsers());
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id) 
        {
            UserVM user = guy.Map(adminLogic.GetUserById(id));
            ViewBag.UserId = user.UserId;
            ViewBag.UserName = user.UserName;
            ViewBag.SecLev = user.SecLev;
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserVM user) //Creates a user
        {
            try
            {
                adminLogic.CreateUser((guy.Map(user)));
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception s)
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            UserVM user = guy.Map(adminLogic.GetUserById(id));
            //Session["EditId"] = user.UserId;
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(UserVM user, int id) //Edits a User
        {
            try
            {
                //user.UserId = (int)Session["EditId"];
                user.UserId = id;
                user.Password = guy.Map(adminLogic.GetUserById(id)).Password;
                adminLogic.EditUserById(guy.Map(user));
                return RedirectToAction("Index");
            }
            catch (Exception r)
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            UserVM user = guy.Map(adminLogic.GetUserById(id));
            //Session["DeleteId"] = user.UserId;
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(UserVM user, int id) //Deletes User
        {
            try
            {
                // TODO: Add delete logic here
                adminLogic.RemoveUserById(id);
                return RedirectToAction("Index", "Admin");
            }
            catch(Exception z)
            {
                return View();
            }
        }
        
        //GET: User/UpdatePassword
        public ActionResult UpdatePassword(int editId)
        {
            PasswordVM pass = new PasswordVM();
            pass.Id = editId;
            return View(pass);
        }
        //POST: User/UpdatePassword
        [HttpPost]
        public ActionResult UpdatePassword(UserVM user)
        {
            try
            {
                adminLogic.UpdatePassword(guy.Map(user));
                return RedirectToAction("Index");
            }
            catch (Exception f)
            {
                return View();
            }

        }
    }
}
