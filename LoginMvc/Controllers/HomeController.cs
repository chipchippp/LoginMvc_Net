using LoginMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginMvc.Controllers
{
    public class HomeController : Controller
    {
        LoginModel db = new LoginModel();
        public ActionResult Index()
        {
            return View();
        }
       
        // Get: /Home/Register
        public ActionResult Register()
        {
            return View();
        }
        // Post: /Home/Register
        [HttpPost]
        public ActionResult Register(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

        // Get: /Home/Login
        public ActionResult Login()
        {
            return View();
        }
        // Post: /Home/Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            var accountForm = user.TaiKhoan;
            var passForm = user.MatKhau;
            var userCheck = db.Users.FirstOrDefault(x => x.TaiKhoan.Equals(accountForm) && x.MatKhau.Equals(passForm));
            if (userCheck != null)
            {
                Session["User"] = userCheck;
                return RedirectToAction("Index", "Home");
            } else
            {
                ViewBag.LoginFail = "Login thất bại, please check !";
                return View("Login");
            }
        }
        // Post: /Home/Logout

        public ActionResult Logout(User user)
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }


    }
}