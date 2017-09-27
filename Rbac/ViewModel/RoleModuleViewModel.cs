using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rbac.ViewModel
{
    public class RoleModuleViewModel
    {
        public int roleid { get; set; }
        public int moduleid { get; set; }
        public int Upmoduleid { get; set; }
        public string roleNume { get; set; }
        public string moduleNume { get; set; }
    }
}