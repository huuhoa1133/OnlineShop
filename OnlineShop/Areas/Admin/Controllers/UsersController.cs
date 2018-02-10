using Model.DAO;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        UserDAO UserDao;
        public UsersController()
        {
            UserDao = new UserDAO();
        }

        // GET: Admin/Users
        public ActionResult Index(int page = 1, int pagesize = 10, string search_string = "")
        {

            var users = UserDao.GetAll(page, pagesize,search_string);
            ViewBag.search_string = search_string;
            return View(users);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

   

        [HttpPost]
        public ActionResult Create (User user)
        {
            if (!ModelState.IsValid)
                return View("Index");
            user.Password = Encryption.MD5Hash(user.Password);
            var dao = new UserDAO();
            long id = UserDao.Insert(user);
            if (id > 0)
                return RedirectToAction("Index", "Users");
            ModelState.AddModelError("", "Thêm user ko thành công");
            return View("Index");
        }

        public ActionResult Edit(long userId)
        {
            var user = UserDao.FindOne(userId);
            return View(user);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }

        public ActionResult test()
        {
            return View();
        }
    }
}