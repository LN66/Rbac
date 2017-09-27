using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rbac.Models;

namespace Rbac.Controllers
{
    public class UserController : Controller
    {
        RbacDb db = new RbacDb();
        // GET: User
        public ActionResult Index()
        {
            return View(db.Users);
        }
        public ActionResult Edit(int id)
        {
            var user = db.Users.FirstOrDefault(m => m.Id == id);
            if (user == null) return Content("编辑项未找到");
            return View(user);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save(User user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            db.Users.AddOrUpdate(user);
            db.SaveChanges();
            return Json(new { code = 200 });
        }
        public ActionResult delete(int id)
        {
            User user = new User();
            //var module = rb.Modules.FirstOrDefault(m => m.ID == id);
            user.Id = id;
            db.Users.Attach(user);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}