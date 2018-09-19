using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IMS.Data
{
    class Account
    {
       
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

        //    public int? Title_id { get; set; }
        public string Title { get; set; }
        [MaxLength(1)]
        public string Gender { get; set; }

        public DateTime? Birthday { get; set; }

        [MaxLength(30)]
        public string Tel { get; set; }

        [MaxLength(1)]
        public string IsLockedOut { get; set; }

        [MaxLength(1)]
        public string IsDoctor { get; set; }

        public string ImagePath { get; set; }

        [Column(TypeName = "ntext")]
        public string Comment { get; set; }

        [Column(TypeName = "ntext")]
        public string Experience { get; set; }
        [Column(TypeName = "ntext")]
        public string Major { get; set; }
        [Column(TypeName = "ntext")]
        public string Expertise { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModDate { get; set; }

        [MaxLength(100)]
        public string ModUser { get; set; }
    }
}
