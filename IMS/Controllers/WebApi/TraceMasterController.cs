using IMS.Models;
using IMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IMS.Controllers.WebApi
{
  public class TraceMasterController : BaseApiController
  {
    // GET api/<controller>
    public IQueryable<TraceMaster> Get(string id)
    {
      return IMSdb.TraceMaster.Where(m => m.TraceNo == id);
    }

    // GET api/<controller>/5
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<controller>
    public IHttpActionResult Post([FromBody]TraceMasterVM traceMasterVM)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      TraceMaster item = new TraceMaster()
      {
        TraceNo = traceMasterVM.TraceNo + "-" +GetTraceNoSeqNo().ToString(),
        PigCnt = traceMasterVM.PigCnt,
        PigFarmId = traceMasterVM.PigFarmId,
        CreDate = DateTime.Now,
        CreUser = User.ID,
        Status = "T",
        BornDate = traceMasterVM.BornDate_s,
        BornDate_end = traceMasterVM.BornDate_e,
      };

      IMSdb.TraceMaster.Add(item);

      try
      {
        IMSdb.SaveChanges();
      }
      catch (DbUpdateException)
      {
        if (IMSdb.TraceMaster.Count(s => s.TraceNo == item.TraceNo) > 0)
        {
          return Conflict();
        }
        else
        {
          throw;
        }
      }

      return CreatedAtRoute("DefaultApi", new { id = item.TraceNo }, item);
    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    public IHttpActionResult Delete(int id)
    {
      TraceMaster item = IMSdb.TraceMaster.Find(id);
      if (item == null)
        return NotFound();

      IMSdb.TraceMaster.Remove(item);
      IMSdb.SaveChanges();

      return Ok(item);

    }
  }
}