﻿using System.ComponentModel.DataAnnotations;

namespace Demo_Product.Models
{
    public class UserRegisterViewModel : ValidationAttribute
    {
        [Required(ErrorMessage = "Lütfen adınızı girin")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı girin")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcı adınızı girin")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen mail adresinizi girin")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Lütfen Şifernizi girin")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi tekrar girin")]
        [Compare("Password",ErrorMessage ="Şifrelerin eşleştiğinden emin olun.")]
        public string ConfirmPassword { get; set; }
    }
}
