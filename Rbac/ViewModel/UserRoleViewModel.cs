using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rbac.ViewModel
{
    public class UserRoleViewModel
    {
        public int roleid { get; set; }
        public int userid { get; set; }
        public int Uproleid { get; set; }
        public string roleNume { get; set; }
        public string userNume { get; set; }
    }
}