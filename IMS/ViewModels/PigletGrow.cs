using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.ViewModels
{
  public class PigletGrow
  {

    public PigletGrow()
    {
      this.TraceNo = PigType + BornDate_s.ToString("yyyyMMdd") + BornDate_e.ToString("yyyyMMdd") + "-" + SeqNo;
    }


    public string PigType { get; set; }

    public string TraceNo {
      get {
        return PigType + BornDate_s.ToString("yyyyMMdd") + BornDate_e.ToString("yyyyMMdd") + "-" + SeqNo;
      }
      set { }
    }

    public int SeqNo { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime BornDate_s { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime BornDate_e { get; set; }

    public int PigletCnt { get; set; }




  }
}