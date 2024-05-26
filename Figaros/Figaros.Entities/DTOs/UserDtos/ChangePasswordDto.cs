using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Figaros.Entities.DTOs.UserDtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string Id { get; set; }

        [DisplayName("Köhnə parol")]
        [Required(ErrorMessage = "{0}u daxil edin!")]
        [MaxLength(20, ErrorMessage = "{0} {1} uzunluqdan çox olmamalıdır!")]
        [MinLength(8, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DisplayName("Yeni parol")]
        [Required(ErrorMessage = "{0}u daxil edin!")]
        [MaxLength(20, ErrorMessage = "{0} {1} uzunluqdan çox olmamalıdır!")]
        [MinLength(8, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni parol")]
        [Required(ErrorMessage = "{0}u təsdiqləyin!")]
        [MaxLength(20, ErrorMessage = "{0} {1} uzunluqdan çox olmamalıdır!")]
        [MinLength(8, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string NewConfirmPassword { get; set; }

    }
}
