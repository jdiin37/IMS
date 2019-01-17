using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace IMS.Models
{
    public class Photo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PigFarmId { get; set; }
        
        //Annotations & Validation
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(100)]
        [DisplayName("主題")]
        public string Title { get; set; }


        [MaxLength]
        [DisplayName("上傳照片")]
        public byte[] PhotoFile { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        //[DataType(DataType.MultilineText)]
        //[StringLength(400)]
        [Required]
        [DisplayName("描述")]
        [DataType(DataType.MultilineText), StringLength(400)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DisplayName("建立日期")]
        public DateTime PostDate { get; set; }

        [Required]
        //1.3 在UserName屬性採用自訂驗證擴充 CheckUsername
        [CheckUsername]
        [DisplayName("發表人名稱")]
        public string PostName { get; set; }


        [DisplayName("建立日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CreDate { get; set; }

        [DisplayName("建立人員")]
        public string CreUser { get; set; }
    }

    //1.2 自訂驗證擴充 CheckUsername Model,繼承ValidationAttribute
    public class CheckUsername : ValidationAttribute
    {
        //1.2.2 在Constructor給ErrorMessage初始值
        public CheckUsername()
        {
            ErrorMessage = "發表人名稱至少2個字";
        }
        //1.2.1 override  IsValid 加入自訂規則
        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return false;
            }
            return (value.ToString().Length >= 2) ? true : false;
        }
    }
}