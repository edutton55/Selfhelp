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
    public class UserController : Controller
    {
        public IUserLogic userLogic = new UserLogic(new UserDAO(new LoggerIO()), new SQLDAO(), new LoggerIO(), new Hashing());
        public IQuoteLogic quoteLogic = new QuoteLogic(new QuoteDAO(new LoggerIO()), new SQLDAO(), new LoggerIO());
        static UserVM guy = new UserVM();

        //// GET: User
        //public ActionResult Index()
        //{
        //    List<UserVM> users = UserVM.Map(userLogic.GetUsers());
        //    return View(users);
        //}

        //// GET: User/Details/5
        //public ActionResult Details(int id)
        //{
        //    UserVM user = UserVM.Map(userLogic.GetUserById(id));
        //    ViewBag.Id = user.UserId;
        //    ViewBag.UserName = user.UserName;
        //    ViewBag.SecLev = user.SecLev;
        //    return View(user);
        //}

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserVM user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userLogic.CreateUser(guy.Map(user));
                    return RedirectToAction("Login");
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(user);
            }
            catch(Exception v)
            {
                return View();
            }
        }
        // GET: User/Login

        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(UserVM user)
        {
            try
            {
                //List<QuoteVM> quotes = QuoteVM.Map(quoteLogic.GetQuotes());  //resets the  PrevRate on log in so the quotes can be rated again
                //foreach (QuoteVM quote in quotes)
                //{
                //    quote.PrevRate = 0;
                //    quoteLogic.EditQuoteById(QuoteVM.Map(quote));
                //}
                UserVM tempUser = guy.Map(userLogic.GetUser(guy.Map(user)));
                if (userLogic.Login(guy.Map(tempUser)))
                {
                    Session["UserId"] = tempUser.UserId;
                    Session["ProfileId"] = tempUser.UserId;
                    Session["UserName"] = tempUser.UserName;
                    Session["Role"] = tempUser.SecLev;
                    ViewBag.User = tempUser.UserName;
                    if (tempUser.SecLev == "User")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (tempUser.SecLev == "Power User")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (tempUser.SecLev == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: User/Login
        public ActionResult LogOut()
        {
            Session["UserId"] = null;
            return RedirectToAction("Welcome", "Home");
        }

        //GET:  User/Profile
        public ActionResult Profile()
        {
            UserVM user = guy.Map(userLogic.GetUserById((int)Session["ProfileId"]));
            return View(user);
        }
    }
}
