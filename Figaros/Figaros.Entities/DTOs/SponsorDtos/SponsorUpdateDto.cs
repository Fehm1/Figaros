using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Figaros.Entities.DTOs.SponsorDtos
{
    public class SponsorUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Şirkət şəkil")]
        [DataType(DataType.Upload)]
        public IFormFile? CompanyImageFile { get; set; }

        [DisplayName("Şirkət şəkil sətri")]
        public string CompanyImageString { get; set; }

        [DisplayName("Aktivdir?")]
        public bool IsActive { get; set; }
    }
}
