using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class Account
    {
        [Key, Column(Order = 1)]
        public int Id { get; set; }

        [Key, Column(Order = 2)]
        [MaxLength(12, ErrorMessage = "長度不得超過12")]
        public string AccountNo { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public DateTime CreDate { get; set; }

        public string CreUser { get; set; }

        public DateTime? ModDate { get; set; }

        public string ModUser { get; set; }
        
    }
}