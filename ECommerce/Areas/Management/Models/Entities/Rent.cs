using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Management.Models.Entities
{
    public enum rentState
    {
        Beklemede,
        Onaylandı,
        iptal
    }

    public class Rent
    {
        public int Id { get; set; }
        public int customerId { get; set; }
        public int productId { get; set; }
        public Nullable<DateTime> rentStartDate { get; set; }
        public Nullable<DateTime> rentEndDate { get; set; }
        public rentState rentState { get; set; }

        public virtual Product  Product{ get; set; }
        public virtual Customer Customer { get; set; }
    }
}