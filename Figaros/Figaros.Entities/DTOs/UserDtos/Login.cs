using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Figaros.Entities.DTOs.UserDtos
{
    public class Login
    {
        [DisplayName("Username")]
        [Required(ErrorMessage = "{0}u daxil edin!")]
        [MaxLength(50, ErrorMessage = "{0} {1} uzunluqdan çox olmamalıdır!")]
        [MinLength(3, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string UserName { get; set; }

        [DisplayName("Parol")]
        [Required(ErrorMessage = "{0}u daxil edin!")]
        [MaxLength(20, ErrorMessage = "{0} {1} uzunluqdan çox olmamalıdır!")]
        [MinLength(8, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
