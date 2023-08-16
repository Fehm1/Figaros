using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Figaros.Entities.DTOs.AboutDtos
{
    public class AboutUpdateDto
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
