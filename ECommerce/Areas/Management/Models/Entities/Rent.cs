using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Areas.Management.Models.Entities
{
    public enum rentState
    {
        Beklemede,
        Onaylandı,
        iptal,
        Uygun
    }

    public class Rent
    {
        public int Id { get; set; }
        public int? custId { get; set; }
        public int productId { get; set; }
        [Display(Name ="Başlangıç Tarihi")]
        [DataType(DataType.Date)]
        
        public Nullable<DateTime> rentStartDate { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        [DataType(DataType.Date)]

        public Nullable<DateTime> rentEndDate { get; set; }
        public rentState rentState { get; set; }

        public virtual Product  Product{ get; set; }
        public virtual Customer Customer { get; set; }

    }
}