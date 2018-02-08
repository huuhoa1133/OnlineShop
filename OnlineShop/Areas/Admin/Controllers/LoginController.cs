using Model.DAO;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var dao = new UserDAO();
            var result = dao.Login(model.UserName, Encryption.MD5Hash(model.Password));
            if (result == null)
            {
                ModelState.AddModelError("", "username or password is valid");
                return View("Index");
            }
            var userSession = new UserLogin()
            {
                UserID = result.ID,
                UserName = result.UserName
            };
            Session.Add(CommonConstants.User_SESSION, userSession);
            return RedirectToAction("Index", "Home");
        }
    }
}