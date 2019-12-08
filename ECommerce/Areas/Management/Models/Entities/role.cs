using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Management.Models.Entities
{
    public class role
    {
        public int roleId { get; set; }
        public string roleName { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
    }
}