using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.ViewModels
{
    public class AccountVM
    {
        public List<Account> accounts { get; set; }
        public List<AccountLevel> accountLevels { get; set; }
    }
}