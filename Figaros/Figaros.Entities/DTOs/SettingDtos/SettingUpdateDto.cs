using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Figaros.Entities.DTOs.SettingDtos
{
    public class SettingUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Header logo")]
        [DataType(DataType.Upload)]
        public IFormFile? HeaderLogoFile { get; set; }

        [DisplayName("Header logo sətri")]
        public string HeaderLogoString { get; set; }

        [DisplayName("Footer logo")]
        [DataType(DataType.Upload)]
        public IFormFile? FooterLogoFile { get; set; }

        [DisplayName("Footer logo sətri")]
        public string FooterLogoString { get; set; }

        [DisplayName("Kontakt şəkil")]
        [DataType(DataType.Upload)]
        public IFormFile? ContactImageFile { get; set; }

        [DisplayName("Kontakt şəkil sətri")]
        public string ContactImageString { get; set; }

        [DisplayName("Ünvan")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(300, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Location { get; set; }

        [DisplayName("Bazar ertəsi - Cümə günü iş saatı")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(100, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string MondayFridayWorkHours { get; set; }

        [DisplayName("Şənbə günü iş saatı")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(100, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string SaturdayWorkHours { get; set; }

        [DisplayName("Bazar günü iş saatı")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(100, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string SundayWorkHours { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(200, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Email { get; set; }

        [DisplayName("Telefon")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(50, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Phone { get; set; }

        [DisplayName("İnstagram")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(100, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string InstagramUrl { get; set; }

        [DisplayName("WhatsApp")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(100, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string WhatsAppUrl { get; set; }

        [DisplayName("Youtube")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(100, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string YoutubeUrl { get; set; }

        [DisplayName("Twitter")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(100, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string TwitterUrl { get; set; }

        [DisplayName("Facebook")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(100, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string FacebookUrl { get; set; }

        [DisplayName("Tiktok")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(100, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string TiktokUrl { get; set; }
        
        [DisplayName("Müraciət aktivdir?")]
        public bool IsActiceRequest { get; set; }

        [DisplayName("Aktivdir?")]
        public bool IsActive { get; set; }
    }
}
