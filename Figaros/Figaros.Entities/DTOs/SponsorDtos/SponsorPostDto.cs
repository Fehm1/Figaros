using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace Figaros.Entities.DTOs.SponsorDtos
{
    public class SponsorPostDto
    {
        [DisplayName("Şirkət şəkil")]
        [DataType(DataType.Upload)]
        public IFormFile? CompanyImageFile { get; set; }

        [DisplayName("Şirkət adı")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(150, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string CompanyName { get; set; }
    }
}
