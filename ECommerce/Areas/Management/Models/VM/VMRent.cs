using ECommerce.Areas.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Management.Models.VM
{
    public class VMRent
    {
        public int Id { get; set; }
        public DateTime rentStartDate { get; set; }
        public DateTime rentEndDate { get; set; }
        public Product Product { get; set; }
       
    }
}