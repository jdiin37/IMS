using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.ViewModels
{
    public class CategoryVM
    {
        public List<Category> categoryLists { get; set; }
        public List<CategorySub> categorySubs { get; set; }
    }
}