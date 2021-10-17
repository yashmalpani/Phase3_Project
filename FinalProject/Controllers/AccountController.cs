using System.Linq;
using FinalProject.Helpers;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {
        ApplicationDBContext context;
        public AccountController()
        {
            context = new ApplicationDBContext();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var userObj = context.Users.Where(u => u.Username == user.Username
            && u.Password == user.Password).SingleOrDefault();
            
            if (userObj != null)
            {
                var temp = userObj.UserType;
                SessionHelper.setObjectAsJson(HttpContext.Session, "user", userObj);
                User usr = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
                if (temp == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Please Enter Your Credentials.";
                return View("Index");
            }
        }

        public IActionResult Register()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
