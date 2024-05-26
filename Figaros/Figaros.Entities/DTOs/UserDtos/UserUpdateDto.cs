using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Figaros.Entities.DTOs.UserDtos
{
    public class UserUpdateDto
    {
        [Required]
        public string Id { get; set; }

        [DisplayName("Rol")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        public string RoleId { get; set; }

        [DisplayName("Şəkil")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }

        public string ImageString { get; set; }

        [DisplayName("Ad")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(200, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Name { get; set; }

        [DisplayName("Soyad")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(200, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Surname { get; set; }

        [DisplayName("Istifadəçi adı")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(200, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(200, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Email { get; set; }

        [DisplayName("Telefon")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(30, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Phone { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }

        [DisplayName("Aktivdir?")]
        public bool IsActive { get; set; }
    }
}
