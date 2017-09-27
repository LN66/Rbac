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
    public class UserRoleController : Controller
    {
        RbacDb db = new RbacDb();
        // GET: UserRole
        public ActionResult Index()
        {
            var userrole = db.Users.Include(r => r.Roles);
            return View(userrole);
        }
        public ActionResult Edit(UserRoleViewModel userrole)
        {
            userrole.roleNume = db.Roles.FirstOrDefault(r => r.Id == userrole.roleid).Name;
            userrole.userNume = db.Users.FirstOrDefault(r => r.Id == userrole.userid).UserName;

            ViewBag.Roleoptions = from r in db.Roles select new SelectListItem { Text = r.Name, Value = r.Id.ToString() };
            return View(userrole);
        }
        public ActionResult Create()
        {
            ViewBag.Roleoptions = from r in db.Roles select new SelectListItem { Text = r.Name, Value = r.Id.ToString() };
            ViewBag.Useroptions = from r in db.Users select new SelectListItem { Text = r.UserName, Value = r.Id.ToString() };
            return View();
        }
        public ActionResult Insert(UserRoleViewModel userrole)
        {
            var user = db.Users.FirstOrDefault(r => r.Id == userrole.userid);
            var role = new Role { Id = userrole.roleid };
            db.Roles.Attach(role);
            user.Roles.Add(role);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }

        public ActionResult delete(UserRoleViewModel userrole)
        {
            var user = db.Users.FirstOrDefault(r => r.Id == userrole.userid);
            var role = new Role { Id = userrole.roleid };
            db.Roles.Attach(role);
            user.Roles.Remove(role);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }

        public ActionResult Update(UserRoleViewModel userrole)
        {
            if (userrole.roleid == userrole.Uproleid)
            {
                return Json(new { code = 400 });
            }
            var user = db.Users.FirstOrDefault(r => r.Id == userrole.userid);

            var role = new Role { Id = userrole.roleid };
            db.Roles.Attach(role);

            var uproleid = new Role { Id = userrole.Uproleid };
            db.Roles.Attach(uproleid);
            user.Roles.Remove(role);
            user.Roles.Add(uproleid);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }
    }
}