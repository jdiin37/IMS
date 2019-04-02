using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IMS.Controllers.WebApi
{
  public class TraceDetailController : BaseApiController
  {
    // GET api/<controller>
    public IQueryable<TraceDetail> Get(string id)
    {
      return IMSdb.TraceDetail.Where(m => m.TraceNo == id);
    }

    // GET api/<controller>/5
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<controller>
    public void Post([FromBody]string value)
    {

    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    public IHttpActionResult Delete(Guid id)
    {
      TraceDetail item = IMSdb.TraceDetail.Find(id);
      if (item == null)
        return NotFound();

      IMSdb.TraceDetail.Remove(item);
      IMSdb.SaveChanges();

      return Ok(item);

    }
  }
}