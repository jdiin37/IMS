using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
  public class TradeRecord
  {
    [Key]
    public int SeqNo { get; set; }
   
    public int TradePigCnt { get; set; }
    
    public double SumWeight { get; set; }

    public double avgWeight { get; set; }

    public int TradePrice { get; set; }

    public double avgPrice { get; set; }
    
    public string TradeTo { get; set; }

    public string TradeMemo { get; set; }

    public string TraceNos { get; set; }

    [Required]
    [DisplayName("建立日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime CreDate { get; set; }

    [DisplayName("建立人員")]
    public string CreUser { get; set; }

    [DisplayName("修改日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime? ModDate { get; set; }

    [DisplayName("修改人員")]
    public string ModUser { get; set; }
    
  }
}