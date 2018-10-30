using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class ActionLog
    {
        [Key]    
        public int ActionLogSN { get; set; }
        
        public DateTime LogTime { get; set; }

        [MaxLength(12)]
        public string CreateBy { get; set; }

        public string AreaName { get; set; }

        public string ControlName { get; set; }

        public string ActionName { get; set; }

        public string Parame { get; set; }

    }
}