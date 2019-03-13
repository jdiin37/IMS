using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace IMS.Models
{
    public class PigBasic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid SeqNo { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(20, ErrorMessage = "長度不得超過12"), MinLength(4, ErrorMessage = "長度不得小於4")]
        [Display(Name = "PigNo", ResourceType = typeof(Resources.Resource))]
        public string PigNo { get; set; }

        [Display(Name = "PigFarm", ResourceType = typeof(Resources.Resource))]
        public string PigFarmId { get; set; }

        [MaxLength]
        [DisplayName("照片")]
        public byte[] PhotoFile { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }


        [Display(Name = "PigType", ResourceType = typeof(Resources.Resource))]
        public string PigType { get; set; }

        [Display(Name = "CommentBody", ResourceType = typeof(Resources.Resource))]
        public string CommentBody { get; set; }

        [Display(Name = "CommentFront", ResourceType = typeof(Resources.Resource))]
        public string CommentFront { get; set; }

        [Display(Name = "CommentEnd", ResourceType = typeof(Resources.Resource))]
        public string CommentEnd { get; set; }

        [Display(Name = "CommentSum", ResourceType = typeof(Resources.Resource))]
        public string CommentSum { get; set; }

        [Display(Name = "PigBirth", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? PigBirth { get; set; }

        [Display(Name = "PigDad", ResourceType = typeof(Resources.Resource))]
        public string PigDad { get; set; }

        [Display(Name = "PigMom", ResourceType = typeof(Resources.Resource))]
        public string PigMom { get; set; }

        [Display(Name = "PigGrandDad", ResourceType = typeof(Resources.Resource))]
        public string PigGrandDad { get; set; }

        [Display(Name = "PigGrandMom", ResourceType = typeof(Resources.Resource))]
        public string PigGrandMom { get; set; }

        [Display(Name = "PigGGrandDad", ResourceType = typeof(Resources.Resource))]
        public string PigGGrandDad { get; set; }

        [Display(Name = "PigGGrandMom", ResourceType = typeof(Resources.Resource))]
        public string PigGGrandMom { get; set; }

        [Display(Name = "Parity", ResourceType = typeof(Resources.Resource))]
        public string Parity { get; set; }

        [Display(Name = "SameParity", ResourceType = typeof(Resources.Resource))]
        public string SameParity { get; set; }

        [Display(Name = "FirstMating", ResourceType = typeof(Resources.Resource))]
        public string FirstMating { get; set; }


        [StringLength(2)]
        [DefaultValue("Y")]
        public string Status { get; set; }
        

        [Required]
        [DisplayName("建立日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreDate { get; set; }

        [DisplayName("建立人員")]
        public string CreUser { get; set; }

        [DisplayName("修改日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? ModDate { get; set; }

        [DisplayName("修改人員")]
        public string ModUser { get; set; }


        public PigBasic()
        {
            this.Status = "Y";
        }

    }
}