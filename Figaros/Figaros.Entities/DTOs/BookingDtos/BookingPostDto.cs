using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Figaros.Entities.DTOs.BookingDtos
{
    public class BookingPostDto
    {
        [DisplayName("Əməkdaş")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [Range(0, int.MaxValue)]
        public int EmployeeId { get; set; }

        [DisplayName("Xidmət")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [Range(0, int.MaxValue)]
        public int ServiceId { get; set; }

        [DisplayName("Saat")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [Range(0, int.MaxValue)]
        public int TimeId { get; set; }

        [DisplayName("Ad Soyad")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(150, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Fullname { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(150, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Email { get; set; }

        [DisplayName("Telefon")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(50, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Phone { get; set; }

        [DisplayName("Tarix")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        public DateTime Date { get; set; }

        [DisplayName("Mesaj")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(500, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Message { get; set; }
    }
}
