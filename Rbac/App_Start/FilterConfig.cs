using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rbac.Filters;

namespace Rbac
{
    public class FilterConfig
    {
        public static void RegisterGlobaFilter(GlobalFilterCollection filters)
        {
            filters.Add(new CustomAuthroziAttribute());
        }
    }
}