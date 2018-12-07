using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class AccountSession
    {
        [Key]
        public int SeqNo { get; set; }

        [StringLength(20)]
        public string AccountNo { get; set; }

        public Guid SessionGID { get; set; }
        
        public DateTime CreDate { get; set; }

        public DateTime? LastActionTime { get; set; }

        public DateTime? DeadLineTime { get; set; }

    }
}