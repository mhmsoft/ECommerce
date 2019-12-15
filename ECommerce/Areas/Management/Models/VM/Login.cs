using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ECommerce.Areas.Management.Models.VM
{
    public class Login
    {
        [Display(Name ="Kullanıcı Adı")]
        public string username { get; set; }

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="şifreniz  en az 6 karakter olmalıdır!")]
        public string password { get; set; }

        [Display(Name = "Beni hatırla")]
        public bool rememberMe { get; set; }
    }
}