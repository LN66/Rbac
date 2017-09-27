using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rbac.Filters;
using Rbac.Models;

namespace Rbac.Controllers
{
    [CustomAuthrozi(AuthorizationType = AuthorizationType.Identity)]
    public class homeController : Controller
    {
        // GET: home
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 头部的分部试图
        /// </summary>
        /// <returns></returns>
        public ActionResult Header()
        {
            /* 第一种方式，选择角色
            var user = Session["user"] as User;
            var roles = Session["roles"] as List<Role>;
            var role = Session["role"] as Role;

            List<SelectListItem> roleList = new List<SelectListItem>();
            foreach(var item in roles)
            {
                roleList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = item.Id == role.Id });
            }

            ViewBag.roleList = roleList;
             return PartialView(user);
            */
            var user = Session["user"] as User;
            var role = Session["role"] as Role;
            ViewBag.UserName = user.UserName;
            //不确定对错
            ViewBag.RoleName = role.Name;

            return PartialView();
        }
        /// <summary>
        /// 导航栏的分部试图
        /// 负责查询你所选择的角色的所有模块
        /// 并且把所有的模块渲染出来
        /// </summary>
        /// <returns></returns>
        public ActionResult Nav(int roleId=0)
        {
            //第一种方式，可选择用户角色
            //获取session里的用户角色模块表
            // var roleModules = Session["roleModules"] as Dictionary<int, ICollection<Module>>;
            //获取session里默认的角色
            //  var role = Session["role"] as Role;
            //  var roles = Session["roles"] as List<Role>;


            //根据参数里roleId，获取某一个用户角色的模块表
            //List<Module> modules;
            //if(roleModules.ContainsKey(roleId))
            //{
            //    modules = roleModules[roleId].ToList();
            //    //切换当前角色
            //    Session["role"] = roles[roleId];
            //}
            //else
            //{
            //    modules = roleModules[role.Id].ToList();
            //}


            //var modules = roleModules[roleId];

            //如果模块表为Null，说明当前传递的roleId有误
            //if(modules==null)
            //{
            //就使用session里的roleId
            //    modules = roleModules[role.Id];
            //}

            //第二种角色，只是用默认角色，不选择

            //获取session里默认的角色
            var role = Session["role"] as Role;
            //从默认角色里获取模块
            var modules = role.Modules;


            return PartialView(modules);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}