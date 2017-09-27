using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rbac.Models;
using System.Data.Entity.Migrations;

namespace Rbac.Controllers
{
    public class ModuleController : Controller
    {
        RbacDb db = new RbacDb();
        // GET: Module
        public ActionResult Index()
        {
          
            return View(db.Modules);
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var module = db.Modules.FirstOrDefault(m => m.Id == id);
            if (module == null) return Content("未找到要编辑的模块");


            return View(module);
        }

        public ActionResult Delete(int id)
        {
            //实例化一个module，并且把要删除的Id 初始化
            Module module = new Module();

            module.Id = id;
            //将要删除的实体，先附加到数据上下文，就像从数据库查出来的module一样
            db.Modules.Attach(module);
            //删除实体类
            db.Modules.Remove(module);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Save(Module module)
        {
            if(!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            //新增或更新实体
            db.Modules.AddOrUpdate(module);
            //持久化数据
            db.SaveChanges();
            return Json(new { code = 200 });
        }
    }
}