using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace IMS.Models
{
    public class FarmDataBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
       
        [Required]
        public Guid PigFarmId { get; set; }

        [StringLength(20)]
        public string OwnerName { get; set; }

        public DateTime? OwnerBirth { get; set; }

        public string OwnerAddress { get; set; }

        [StringLength(20)]
        public string OwnerPhone { get; set; }

        [StringLength(20)]
        public string OwnerFax { get; set; }

        public string OwnerEducation { get; set; }

        [StringLength(20)]
        public string FarmName { get; set; }

        public string FarmAddress { get; set; }

        [StringLength(20)]
        public string FarmPhone { get; set; }

        [StringLength(20)]
        public string FarmFax { get; set; }

        [StringLength(2)]
        [DefaultValue("Y")]
        [HiddenInput(DisplayValue = false)]
        public string Status { get; set; }

        [DisplayName("建立日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [HiddenInput(DisplayValue = false)]
        public DateTime CreDate { get; set; }

        [DisplayName("建立人員")]
        [HiddenInput(DisplayValue = false)]
        public string CreUser { get; set; }

        [DisplayName("修改日期")]
        [HiddenInput(DisplayValue = false)]
        public DateTime? ModDate { get; set; }

        [DisplayName("修改人員")]
        [HiddenInput(DisplayValue = false)]
        public string ModUser { get; set; }

        public FarmDataBase()
        {
            this.Status = "Y";
        }

    }
}