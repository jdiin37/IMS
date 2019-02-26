using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.ViewModels
{
    public class TraceVM
    {
        public TraceMaster traceMaster { get; set; }
        public List<TraceDetail> traceDetails { get; set; }
        public PigFarm pigFarm { get; set; }
    }
}