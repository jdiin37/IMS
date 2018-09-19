using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IMS.Data
{
    public class Account
    {
        public Account() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(20)]
        public string UserNo { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModDate { get; set; }

        [MaxLength(100)]
        public string ModUser { get; set; }
        
    }
}
