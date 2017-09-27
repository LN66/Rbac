using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rbac.Models;
using Rbac.Filters;

namespace Rbac.Controllers
{
    [CustomAuthrozi(AuthorizationType =AuthorizationType.None)]
    public class LoginController : Controller
    {
        RbacDb db = new RbacDb();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(User loginUser)
        {
            //模型绑定验证
            if(!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            //查找用户
            var user = db.Users.FirstOrDefault(
                u => u.UserName == loginUser.UserName && u.PassWord == loginUser.PassWord);
            //如果没找到，就返回404
            if (user == null) return Json(new { code = 404 });
            //设置Sessio，作为身份验证的标识
            Session["user"] = user;


            /*第一种方式，支持多角色选择
             * 
            //获取所有角色的所有模块，转换成字典类，key是脚色的id。value是角色所有用的模块集合
            var roleModules = user.Roles.ToDictionary(r => r.Id, r => r.Modules);
            //存入到session，之后在复用，不用再查数据库。
            Session["roleModules"] = roleModules;


            //获取用户角色表
            var roles = user.Roles.ToList();
            //存入到session，以使之后复用
            Session["roles"] = roles;
            //设置默认角色为用户角色表里的第一个
            Session["rele"] = roles[0];
            */

            //Func泛型委托
            //Func<T,T,......TResult>


            //第二种形式，内联委托
            //Func<Role, bool> func1 = delegate (Role role1)
            //   {
            //       if (role1.Id == 3) return true;
            //       return false;
            //   };

            //第三种形式，lambda表达式代替内联委托
            //Func<Role, bool> func1 = robe1 =>
            //  {
            //      return true;
            //  };


            //第四种形式，简化了lambda表达式的方法体，如果只有一句返回值的语句，就可以省略掉{}和return
           //  Func<Role, bool> func1 = robe1 => true;

            //r=>true  就是只拿第一条
            var role = user.Roles.FirstOrDefault(robe1 => true);

            Session["role"] = role;

            return Json(new { code = 200 });


            //Take(1)，就是从一个集合里，只拿出一条数据
           // Session["role"] = user.Roles.Take(1);

            //return Json(new { code = 200 });
        }
        /// <summary>
        /// 第一种委托形式，创建一个方法，传给委托
        /// </summary>
        /// <param name="role1"></param>
        /// <returns></returns>
        private bool Func1(Role role1)
        {
            if (role1.Id == 3) return true;
            return false;
        }
    }
}