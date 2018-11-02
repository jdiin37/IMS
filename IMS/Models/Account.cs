using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class Account
    {
        [Key]
        public int SeqNo { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(20, ErrorMessage = "長度不得超過12"),MinLength(4, ErrorMessage = "長度不得小於4")]
        [Display(Name = "AccountNo", ResourceType = typeof(Resources.Resource))]
        public string AccountNo { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [StringLength(20)]
        public string Password { get; set; }

        [Display(Name = "AccountName", ResourceType = typeof(Resources.Resource))]
        public string AccountName { get; set; }

        [Required]
        public string Email { get; set; }

        [StringLength(2)]
        public string Status { get; set; }

        [Required]
        public DateTime CreDate { get; set; }

        public string CreUser { get; set; }

        public DateTime? ModDate { get; set; }

        public string ModUser { get; set; }

        [Display(Name = "AccountLevel_Level", ResourceType = typeof(Resources.Resource))]
        [Required]
        public string Level { get; set; }
        
        
    }
}