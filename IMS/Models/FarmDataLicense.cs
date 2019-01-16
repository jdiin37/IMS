using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace IMS.Models
{
    public class FarmDataLicense
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PigFarmId { get; set; }


        [DisplayName("經營型態")]
        public string RunType { get; set; }

        [DisplayName("土地容許使用")]
        [StringLength(4)]
        public string LandAllowFlag { get; set; }

        [DisplayName("字號")]
        public string LandAllowNo { get; set; }

        [DisplayName("合法建物使用證明")]
        [StringLength(4)]
        public string BuildingAllowFlag { get; set; }

        [DisplayName("字號")]
        public string BuildingAllowNo { get; set; }

        [DisplayName("牧場登記證書")]
        [StringLength(4)]
        public string FarmLicenseFlag { get; set; }

        [DisplayName("字號")]
        public string FarmLicenseNo { get; set; }

        [DisplayName("排放水許可證")]
        [StringLength(4)]
        public string DrainAllowFlag { get; set; }

        [DisplayName("字號")]
        public string DrainAllowNo { get; set; }

        [DisplayName("廢水處理設備")]
        [StringLength(4)]
        public string WasteWaterDeviceFlag { get; set; }

        [DisplayName("字號")]
        public string WasteWaterDeviceNo { get; set; }

        [DisplayName("容量")]
        public float WasteWaterDeviceSize { get; set; }


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

        public FarmDataLicense()
        {
            this.Status = "Y";
        }
    }
}