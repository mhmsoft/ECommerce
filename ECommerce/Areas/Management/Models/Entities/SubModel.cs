using ECommerce.Areas.Management.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Management.Models.Entities
{
    public class SubModel:BaseEntity
    {
       
        public int modelId { get; set; }
      
        public virtual  Model Model { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}