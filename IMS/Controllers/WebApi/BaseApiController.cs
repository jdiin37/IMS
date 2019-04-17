using IMS.DAL;
using IMS.Models;
using IMS.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IMS.Controllers.WebApi
{
  public class BaseApiController : ApiController
  {
    protected IMSDBContext IMSdb;

    public BaseApiController()
        : base()
    {
      IMSdb = new IMSDBContext();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (IMSdb != null)
        {
          IMSdb.Dispose();
          IMSdb = null;
        }
      }
      base.Dispose(disposing);
    }


    protected virtual new CustomPrincipal User
    {
      get { return base.User as CustomPrincipal; }
    }

    public Account CurrentUser
    {
      get
      {
        return IMSdb.Account.First(m => m.AccountNo == User.ID);
      }
    }


    #region Get SeqNo
    protected String GetTraceNo()
    {
      var lastNo = IMSdb.SeqNo.Where(m => m.Name == "TraceNo").FirstOrDefault();
      DateTime lastDate = lastNo.ModDate;

      if (DateTime.Today > lastDate)
      {
        lastNo.CurrentValue = 0 + lastNo.IncrementValue;
      }
      else
      {
        lastNo.CurrentValue = lastNo.CurrentValue + lastNo.IncrementValue;
      }

      lastNo.ModDate = DateTime.Now;
      IMSdb.SaveChanges();

      return DateTime.Today.ToString("yyyyMMdd") + lastNo.CurrentValue.ToString("000");

    }

    protected int GetTraceNoSeqNo()
    {
      var lastNo = IMSdb.SeqNo.Where(m => m.Name == "TraceNoSeqNo").FirstOrDefault();

      lastNo.CurrentValue = lastNo.CurrentValue + lastNo.IncrementValue;
      lastNo.ModDate = DateTime.Now;
      IMSdb.SaveChanges();

      return lastNo.CurrentValue;
    }
    #endregion
  }
}
