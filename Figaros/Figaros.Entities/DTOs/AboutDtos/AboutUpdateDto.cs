using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Figaros.Entities.DTOs.AboutDtos
{
    public class AboutUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Böyük şəkil")]
        [DataType(DataType.Upload)]
        public IFormFile? BigImageFile { get; set; }

        [DisplayName("Böyük şəkil sətri")]
        public string BigImageString { get; set; }

        [DisplayName("Kiçik şəkil")]
        [DataType(DataType.Upload)]
        public IFormFile? SmallImageFile { get; set; }

        [DisplayName("Kiçik şəkil sətri")]
        public string SmallImageString { get; set; }

        [DisplayName("Başlıq")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(150, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Title { get; set; }

        [DisplayName("Açıqlama")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(500, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Description { get; set; }

        [DisplayName("Aktivdir?")]
        public bool IsActive { get; set; }
    }
}
