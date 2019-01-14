using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace IMS.Models
{
    public class FarmDataLand
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
       
        [Required]
        public Guid PigFarmId { get; set; }

        [DisplayName("牧場土地地號")]
        public string LandNo { get; set; }

        [DisplayName("牧場土地地號2")]
        public string LandNo2 { get; set; }

        [DisplayName("牧場土地地號3")]
        public string LandNo3 { get; set; }

        [DisplayName("土地面積")]
        public string AreaSize { get; set; }

        [DisplayName("土地產權")]
        public string LandProperty { get; set; }

        [DisplayName("都市計畫土地")]
        public string UseIsCity { get; set; }

        [DisplayName("非都市計畫土地")]
        public string UseNonCity { get; set; }

        [DisplayName("非都市土地用地類別")]
        public string NonCityType { get; set; }


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

        public FarmDataLand()
        {
            this.Status = "Y";
        }

    }
}