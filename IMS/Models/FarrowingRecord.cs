using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace IMS.Models
{
    public class FarrowingRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid GID { get; set; }

        [Required]
        public Guid PigGid { get; set; }

        //[DataType(DataType.Date)]
        [Display(Name = "BreedingDate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime BreedingDate { get; set; }

        [Display(Name = "BoarNo", ResourceType = typeof(Resources.Resource))]
        public string BoarNo { get; set; }

        //[DataType(DataType.Date)]
        [Display(Name = "FarrowingDate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? FarrowingDate { get; set; }


        [Display(Name = "BornCnt", ResourceType = typeof(Resources.Resource))]
        public int? BornCnt { get; set; }

        [Display(Name = "BornAliveCnt", ResourceType = typeof(Resources.Resource))]
        public int? BornAliveCnt { get; set; }

        [Display(Name = "BornDeadCnt", ResourceType = typeof(Resources.Resource))]
        public int? BornDeadCnt { get; set; }

        [Display(Name = "WeaningCnt", ResourceType = typeof(Resources.Resource))]
        public int? WeaningCnt { get; set; }

        //[DataType(DataType.Date)]
        [Display(Name = "WeaningDate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? WeaningDate { get; set; }

        [Display(Name = "WeaningAge", ResourceType = typeof(Resources.Resource))]
        public int? WeaningAge { get; set; }


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


        public FarrowingRecord()
        {
            this.Status = "Y";
        }

    }
}
