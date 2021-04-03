using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApp.Db;
using MyApp.Model;
using System.Web.Security;
namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Membership1 model)
        {
            using(var context=new EmployeeDBEntities())
            {
                bool isValid = context.User.Any(x => x.UserName == model.UserName && x.Password == model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("GetAllRecords", "Home");
                }
                else ViewBag.Error = "Incorrect username or Password";
            }
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Membership1 model)
        {
            using(var context=new EmployeeDBEntities())
            {
                 User mem=new User
                {
                    UserName=model.UserName,
                    Password=model.Password
                };
                context.User.Add(mem);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult About()
        {
            return View();
        }
    }
}