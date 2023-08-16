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
    }
}
