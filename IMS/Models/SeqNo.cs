using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class SeqNo
    {
        [Key]
        public string Name { get; set; }
        
        public int CurrentValue { get; set; }
        
        public int IncrementValue { get; set; }

        public int getNewValue()
        {
            return this.CurrentValue + this.IncrementValue;
        }

        [DisplayName("建立日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime ModDate { get; set; }
    }
}