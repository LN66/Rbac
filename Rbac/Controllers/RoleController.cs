using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rbac.Models;

namespace Rbac.Controllers
{
    public class RoleController : Controller
    {
        RbacDb db = new RbacDb();
        // GET: Role
        public ActionResult Index()
        {
            return View(db.Roles);
        }
        public ActionResult Edit(int id)
        {
            var role = db.Roles.FirstOrDefault(m => m.Id == id);
            if (role == null) return Content("编辑项未找到");
            return View(role);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save(Role role)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            db.Roles.AddOrUpdate(role);
            db.SaveChanges();
            return Json(new { code = 200 });
        }
        public ActionResult delete(int id)
        {
            Role role = new Role();
            //var module = rb.Modules.FirstOrDefault(m => m.ID == id);
            role.Id = id;
            db.Roles.Attach(role);
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}