using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Rbac.Models;
using Rbac.ViewModel;


namespace Rbac.Controllers
{
    public class RoleModuleController : Controller
    {
        RbacDb db = new RbacDb();
        // GET: RoleModule
        public ActionResult Index()
        {
            var rolemodule = db.Roles.Include(r => r.Modules);
            return View(rolemodule);
        }
        public ActionResult Edit(RoleModuleViewModel rolemodule)
        {
            rolemodule.roleNume = db.Roles.FirstOrDefault(r => r.Id == rolemodule.roleid).Name;
            rolemodule.moduleNume = db.Modules.FirstOrDefault(r => r.Id == rolemodule.moduleid).Name;

            ViewBag.Moduleoptions = from r in db.Modules select new SelectListItem { Text = r.Name, Value = r.Id.ToString() };
            return View(rolemodule);
        }
        public ActionResult Create()
        {
            ViewBag.Roleoptions = from r in db.Roles select new SelectListItem { Text = r.Name, Value = r.Id.ToString() };
            ViewBag.Moduleoptions = from r in db.Modules select new SelectListItem { Text = r.Name, Value = r.Id.ToString() };
            return View();
        }
        public ActionResult Insert(RoleModuleViewModel rolemodule)
        {
            var role = db.Roles.FirstOrDefault(r => r.Id == rolemodule.roleid);
            var module = new Module { Id = rolemodule.moduleid };
            db.Modules.Attach(module);
            role.Modules.Add(module);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }

        public ActionResult delete(RoleModuleViewModel rolemodule)
        {
            var role = db.Roles.FirstOrDefault(r => r.Id == rolemodule.roleid);
            var module = new Module { Id = rolemodule.moduleid };
            db.Modules.Attach(module);
            role.Modules.Remove(module);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }

        public ActionResult Update(RoleModuleViewModel rolemodule)
        {
            if (rolemodule.moduleid == rolemodule.Upmoduleid)
            {
                return Json(new { code = 400 });
            }
            var role = db.Roles.FirstOrDefault(r => r.Id == rolemodule.roleid);

            var module = new Module { Id = rolemodule.moduleid };
            db.Modules.Attach(module);

            var upmoduleid = new Module { Id = rolemodule.Upmoduleid };
            db.Modules.Attach(upmoduleid);
            role.Modules.Remove(module);
            role.Modules.Add(upmoduleid);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }
    }
}