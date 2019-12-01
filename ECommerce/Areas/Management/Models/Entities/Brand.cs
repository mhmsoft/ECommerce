using ECommerce.Areas.Management.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Management.Models.Entities
{
    public class Brand:BaseEntity
    {
        //one to many relation
        public virtual ICollection<Model> Model { get; set; }
        public virtual ICollection<Product> Product { get; set; }


    }
}