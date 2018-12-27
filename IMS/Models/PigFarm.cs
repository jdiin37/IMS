using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class PigFarm
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        

        public string Status { get; set; }

        public DateTime CreDate { get; set; }

        public string CreUser { get; set; }

        public DateTime? ModDate { get; set; }

        public string ModUser { get; set; }
    }
}