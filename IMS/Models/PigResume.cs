using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class PigResume
    {
        [Key]
        [DisplayName("流水號")]
        public int ID { get; set; }


        [DisplayName("批次編號")]
        public string PigNo { get; set; }

        [DisplayName("豬場編號")]
        public string PigFarmID { get; set; }

        [DisplayName("出生日期")]
        public DateTime BirthDate { get; set; }

        [DisplayName("種類")]
        public string Type { get; set; }

        [DisplayName("屠宰場")]
        public string Slaughterhouse { get; set; }

        public DateTime CreDate { get; set; }

        public string CreUser { get; set; }

        public DateTime ModDate { get; set; }

        public string ModUser { get; set; }

    }
}