using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Figaros.Entities.DTOs.ProductDtos
{
    public class ProductUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Şəkil")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }

        [DisplayName("Şəkil sətri")]
        public string ImageString { get; set; }

        [DisplayName("Başlıq")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(200, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Title { get; set; }

        [DisplayName("Açıqlama")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(500, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Description { get; set; }

        [DisplayName("Dəyər qiyməti")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [Range(0, double.MaxValue)]
        public double CostPrice { get; set; }

        [DisplayName("Satış qiyməti")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [Range(0, double.MaxValue)]
        public double SalePrice { get; set; }

        [DisplayName("Endirim faizi")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [Range(0, double.MaxValue)]
        public double DiscountPercent { get; set; }

        [DisplayName("Məhsul sayı")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [Range(0, int.MaxValue)]
        public int ProductAmount { get; set; }

        [DisplayName("Satış sayı")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [Range(0, int.MaxValue)]
        public int SaleCount { get; set; }

        [DisplayName("Yenidir?")]
        public bool IsNew { get; set; }

        [DisplayName("Populyardır?")]
        public bool IsPopular { get; set; }

        [DisplayName("Aktivdir?")]
        public bool IsActive { get; set; }
    }
}
