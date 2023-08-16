using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Figaros.Entities.DTOs.FAQDtos
{
    public class FAQUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Sual")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(200, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Question { get; set; }

        [DisplayName("Cavab")]
        [Required(ErrorMessage = "{0} daxil edin!")]
        [MaxLength(500, ErrorMessage = "{0} {1} uzunluqdan az olmamalıdır!")]
        public string Answer { get; set; }

        [DisplayName("Aktivdir?")]
        public bool IsActive { get; set; }
    }
}
