using System.ComponentModel.DataAnnotations;

namespace Demo_Product.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı")]
        public string Username { get; set; }
        
        [Required(ErrorMessage ="Şifre")]
        public string Password { get; set; }
    }
}
