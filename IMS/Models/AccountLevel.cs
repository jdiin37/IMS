using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class AccountLevel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "AccountLevel_Level", ResourceType = typeof(Resources.Resource))]
        [Required]
        public string Level { get; set; }

        [Display(Name = "AccountLevel_LevelName", ResourceType = typeof(Resources.Resource))]
        [Required]
        public string LevelName { get; set; }
        
        public string Status { get; set; }

        public DateTime CreDate { get; set; }

        public string CreUser { get; set; }

        public DateTime? ModDate { get; set; }

        public string ModUser { get; set; }
        
    }
}