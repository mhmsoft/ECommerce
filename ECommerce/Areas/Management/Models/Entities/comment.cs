using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Areas.Management.Models.Entities
{
    public class comment
    {
        public int Id { get; set; }
        [Display(Name ="İsim")]
        public string owner { get; set; }
        [Display(Name = "Eposta")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Geçerli bir email adresi giriniz")]
        public string email { get; set; }
        [Display(Name = "Yorum")]
        public string message { get; set; }
        [Display(Name = "Tarih")]
        public DateTime reviewDate { get; set; }

        public int? productId { get; set; }

        public virtual Product Product { get; set; }
    }
}