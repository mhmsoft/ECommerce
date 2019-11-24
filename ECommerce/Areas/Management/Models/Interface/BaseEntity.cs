using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Areas.Management.Models.Interface
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Ad")]
        [Required(ErrorMessage ="Bir ad Belirtmeniz gerekir")]
        public string Name { get; set; }
        [Display(Name="Ürün Oluşturma Tarihi")]
        public DateTime created => DateTime.Now;
    }
}