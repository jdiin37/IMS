using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.ViewModels
{
  


  public class PigBornListVM
  {
    public List<PigBorn> pigBornList;
    
    
  }

  public class PigBorn
  {
    public DateTime? BornDate { get; set; }
    public int? BornCnt { get; set; }
    public Guid MomId { get; set; }
    public string BoarNo { get; set; }
  }
}