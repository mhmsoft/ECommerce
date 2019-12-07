using ECommerce.Areas.Management.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Management.Models.Entities
{
    public class Model:BaseEntity
    {
        //public int parentId { get; set; }
        public int brandId { get; set; }

        //one to one reletion
        public virtual Brand Brand { get; set; }
        public virtual ICollection<SubModel> SubModel { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}