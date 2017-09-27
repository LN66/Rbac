using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rbac.Models;


namespace Rbac.Filters
{
    /// <summary>
    /// 授权验证类型枚举
    /// None,无限制。不要身份认证
    /// Identit，只进行身份认证，不进行授权认证
    /// Authoriza，使用身份和授权认证
    /// </summary>
    public enum AuthorizationType { None, Identity, Authorization}
    /// <summary>
    /// 自定义的授权过滤器
    /// </summary>
    public class CustomAuthroziAttribute : FilterAttribute, IAuthorizationFilter
    {
        public AuthorizationType AuthorizationType { get; set; } = AuthorizationType.Authorization;
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //1.无限制
            if (AuthorizationType == AuthorizationType.None) return;
            //2.身份验证
            if(filterContext.HttpContext.Session["user"]==null)
            {
                RedirectToLogin(filterContext);
                return;
            }
            //如果请求的控制器属于Identity，就不再进行授权，直接返回
            if (AuthorizationType == AuthorizationType.Identity) return;

            //3.授权认证
            //3-1获取当前请求的控制器名称
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //3-2从session里拿到用户登录的角色
            var role = filterContext.HttpContext.Session["role"] as Role;
            //3-3如果角色为空的，说明用户信息不完整，所以返回
            if(role==null)
            {
                RedirectToLogin(filterContext);
                return;
            }
            //3-4查找角色模块里的控制器是否存在，我们请求的控制器
            var module = role.Modules.FirstOrDefault(m => m.Controller == controller);
            //3-5如果不存在，就重定向到登陆页
            if (module == null)
            {
                RedirectToLogin(filterContext);
            }

            //Func<Module,bool> func1=module=>
            //{
            //    return module.Controller != controller;
            //};

            //if (role.Modules.All(m=>m.Controller!=controller))
            //{
            //    RedirectToLogin(filterContext);
            //}


        }
        /// <summary>
        /// 重定向到登陆页面
        /// </summary>
        /// <param name="filterContext"></param>
        public void RedirectToLogin(AuthorizationContext filterContext)
        {
            var url = new UrlHelper(filterContext.RequestContext);
            filterContext.Result = new RedirectResult(url.Action("Index", "Login"));

        }
    }
}