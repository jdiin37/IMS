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

        [DisplayName("負責人")]
        [StringLength(20)]
        public string OwnerName { get; set; }

        [DisplayName("負責人生日")]
        public DateTime? OwnerBirth { get; set; }

        [DisplayName("負責人住址")]
        public string OwnerAddress { get; set; }

        [DisplayName("負責人電話")]
        [StringLength(20)]
        public string OwnerPhone { get; set; }

        [DisplayName("負責人傳真")]
        [StringLength(20)]
        public string OwnerFax { get; set; }

        [DisplayName("負責人教育程度")]
        public string OwnerEducation { get; set; }

        [DisplayName("牧場名稱")]
        [StringLength(20)]
        public string FarmName { get; set; }

        [DisplayName("牧場地址")]
        public string FarmAddress { get; set; }

        [DisplayName("牧場電話")]
        [StringLength(20)]
        public string FarmPhone { get; set; }

        [DisplayName("牧場傳真")]
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