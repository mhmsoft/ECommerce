using ECommerce.Areas.Management.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Management.Models.Entities
{
    public class Model:BaseEntity
    {
        public int parentId { get; set; }
        public int brandId { get; set; }

        public Brand Brand { get; set; }
    }
}