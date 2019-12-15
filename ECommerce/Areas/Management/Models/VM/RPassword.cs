using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Management.Models.VM
{
    public class RPassword
    {
        [Display(Name = "Eposta")]
        [Required(ErrorMessage = "Lütfen bir eposta giriniz")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geçerli bir eposta giriniz")]
        public string email { get; set; }
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen bir şifre giriniz")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifrenizin en az 6 karakter olması gerekir")]
        public string password { get; set; }
        [Display(Name = "Tekrar Şifre")]
        [Required(ErrorMessage = "Lütfen bir Tekrar şifre giriniz")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifrenizin en az 6 karakter olması gerekir")]
        [Compare("password", ErrorMessage = "Şifreniz eşleşmiyor")]
        public string rePassword { get; set; }

        public string resetcode { get; set; }
    }
}