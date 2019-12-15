using ECommerce.Areas.Management.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Areas.Management.Models.Entities
{
    public class SubModel:BaseEntity
    {
       
        public int modelId { get; set; }
        //özellikler
        public bool AirBag { get; set; }
        public bool CDMp3 { get; set; }
        public bool Bluetooth { get; set; }
        public bool Navigation { get; set; }
        public bool ChildLock { get; set; }
        public bool Sunroof { get; set; }

        public virtual  Model Model { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}