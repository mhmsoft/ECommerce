using ECommerce.Areas.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ECommerce.Areas.Management.Models.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext():base("name=MyECommerce")
        {

        }
        // Tables
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
    }
}