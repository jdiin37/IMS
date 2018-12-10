using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace IMS.Models.Auth
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string level)
        {
            if(level == "root")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public string ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string SessionGid { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string SessionGid { get; set; }
    }
}