using System.ComponentModel.DataAnnotations;

namespace CORE.DTOs.ApplicationUser
{
    public class CreateApplicaitonUserDTO
    {
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(200)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Boş Geçilemez")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Doğrulama Boş Geçilemez")]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
