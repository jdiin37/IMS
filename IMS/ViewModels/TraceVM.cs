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

  public class TraceMasterVM
  {
    public string dadType { get; set; }
    public string momType { get; set; }
    public string PigFarmId { get; set; }
    public DateTime BornDate_s { get; set; }
    public DateTime BornDate_e { get; set; }
    public int PigCnt { get; set; }
    public string TraceNo
    {
      get
      {
        return momType + dadType + BornDate_s.ToString("yyyyMMdd") + BornDate_e.ToString("yyyyMMdd");
      }
      set { }
    }
  }
}