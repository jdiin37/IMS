using IMS.DAL;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IMS.Controllers.WebApi
{
  public class FarrowingRecordController : BaseApiController
  {
    
    // GET: api/FarrowingRecord
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET: api/FarrowingRecord/5
    public string Get(int id)
    {
      return "value";
    }

    // POST: api/FarrowingRecord
    public void Post([FromBody]string value)
    {
    }

    // PUT: api/FarrowingRecord/5
    public IHttpActionResult PUT(string id, [FromBody]FarrowingRecord farrowingRecord)
    {
      //if (!ModelState.IsValid)
      //{
      //  return BadRequest(ModelState);
      //}
      //if (gid != farrowingRecord.GID)
      //{
      //  return BadRequest();
      //}
      Guid gid = new Guid(id);

      try
      {
        FarrowingRecord item = IMSdb.FarrowingRecord.Find(gid);
        if (item != null)
        {
          item.ModDate = DateTime.Now;
          item.ModUser = User.ID;

          item.BreedingDate = farrowingRecord.BreedingDate;
          item.BoarNo = farrowingRecord.BoarNo;
          item.FarrowingDate = farrowingRecord.FarrowingDate;
          item.BornCnt = farrowingRecord.BornCnt;
          item.BornAliveCnt = farrowingRecord.BornAliveCnt;
          item.BornDeadCnt = farrowingRecord.BornDeadCnt;
          item.WeaningDate = farrowingRecord.WeaningDate;
          item.WeaningCnt = farrowingRecord.WeaningCnt;

          item.WeaningAge = farrowingRecord.WeaningAge;
          IMSdb.SaveChanges();
        }
      }
      catch (DbUpdateException)
      {
        if (!(IMSdb.FarrowingRecord.Any(s => s.GID == farrowingRecord.GID)))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return StatusCode(HttpStatusCode.NoContent);
    }

    // DELETE: api/FarrowingRecord/5
    public void Delete(Guid gid)
    {
    }
  }
}
