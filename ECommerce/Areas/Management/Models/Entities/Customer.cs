using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Areas.Management.Models.Entities
{
    public class Customer
    {
        [Key]
        public int customerId { get; set; }
        [Display(Name ="Ad")]
        public string firstName { get; set; }
        [Display(Name = "Soyad")]
        public string lastName { get; set; }
        [Display(Name = "Eposta")]
        [Required(ErrorMessage ="Lütfen bir eposta giriniz")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Geçerli bir eposta giriniz")]
        public string email { get; set; }
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen bir şifre giriniz")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="Şifrenizin en az 6 karakter olması gerekir")]
        public string password { get; set; }
        [Display(Name = "Tekrar Şifre")]
        [Required(ErrorMessage = "Lütfen bir Tekrar şifre giriniz")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifrenizin en az 6 karakter olması gerekir")]
        [Compare("password",ErrorMessage ="Şifreniz eşleşmiyor")]
        public string rePassword { get; set; }
        [Display(Name = "Tekrar Şifre")]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }
        [Display(Name = "Adres")]
        public string address { get; set; }
        [Display(Name = "Şehir")]
        public string city { get; set; }
       
        public string activationCode { get; set; }
        public string resetCode { get; set; }
        public string hostName { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> loginAttempt { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<System.DateTime> loginTime { get; set; }
        public Nullable<bool> isMailVerified { get; set; }
        [Display(Name = "Abonelik")]
        public Nullable<bool> subscribe { get; set; }

         public Nullable<int> roleId { get; set; }
        public virtual role role { get; set; }
    }
}