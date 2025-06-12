using System.ComponentModel.DataAnnotations;

namespace WEBUI.Models
{
    public class ResetPasswordViewModel
    {
        public string token { get; set; }
        public string userId { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Boş Geçilemez")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Boş Geçilemez")]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
