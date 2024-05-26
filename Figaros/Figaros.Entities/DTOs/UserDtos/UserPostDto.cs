using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Figaros.Entities.DTOs.UserDtos
{
    public class UserPostDto
    {
        [DisplayName("Rol")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        public string RoleId { get; set; }

        [DisplayName("Şəkil")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }

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

        [DisplayName("Parol")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(20, ErrorMessage = "{0} {1} uzunluqdan çox olmamalıdır!")]
        [MinLength(8, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Parol")]
        [Required(ErrorMessage = "{0}u təsdiqləyin!")]
        [MaxLength(20, ErrorMessage = "{0} {1} uzunluqdan çox olmamalıdır!")]
        [MinLength(8, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }

    }
}
