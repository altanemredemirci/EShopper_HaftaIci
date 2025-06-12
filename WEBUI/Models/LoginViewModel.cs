using System.ComponentModel.DataAnnotations;

namespace WEBUI.Models
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        [StringLength(200)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Şifre Boş Geçilemez")]
        public string Password { get; set; }

        public bool IsPersient { get; set; }
    }
}
