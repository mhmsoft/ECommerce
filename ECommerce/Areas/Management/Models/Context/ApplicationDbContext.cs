using ECommerce.Areas.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<SubModel> SubModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Product>()
            .HasRequired(c => c.Model)
            .WithMany()
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
           .HasRequired(c => c.SubModel)
           .WithMany()
           .WillCascadeOnDelete(false);
        }
    }
}