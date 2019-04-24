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
  public class TradeRecordController : BaseApiController
  {
    // GET api/<controller>
    public IQueryable<TradeRecord> Get()
    {
      
      return IMSdb.TradeRecord.Select(m=>m);
    }

    // GET api/<controller>/5
    public IHttpActionResult Get(int id)
    {

      TradeRecord item = IMSdb.TradeRecord.Where(m => m.SeqNo == id).FirstOrDefault();
      return Ok(item);
    }

    // POST api/<controller>
    public IHttpActionResult Post([FromBody]TradeRecord tradeRecord)
    {
      //if (!ModelState.IsValid)
      //{
      //  return BadRequest(ModelState);
      //}

      TradeRecord item = new TradeRecord()
      {
        TradePigCnt = tradeRecord.TradePigCnt,
        TradePrice = tradeRecord.TradePrice,
        TraceNos = tradeRecord.TraceNos,
        TradeTo = tradeRecord.TradeTo,
        TradeMemo = tradeRecord.TradeMemo,
        SumWeight = tradeRecord.SumWeight,
        avgPrice = tradeRecord.avgPrice,
        avgWeight = tradeRecord.avgWeight,
        CreDate = DateTime.Now,
        CreUser = User.ID,
      };

      IMSdb.TradeRecord.Add(item);

      try
      {
        IMSdb.SaveChanges();
      }
      catch (DbUpdateException)
      {
        
        return Conflict();
      }

      return Ok(item);
    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    public IHttpActionResult Delete(int id)
    {
      TradeRecord item = IMSdb.TradeRecord.Find(id);
      if (item == null)
        return NotFound();

      IMSdb.TradeRecord.Remove(item);
      IMSdb.SaveChanges();

      return Ok(item);

    }
  }
}