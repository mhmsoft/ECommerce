namespace ECommerce.Migrations
{
    using ECommerce.Areas.Management.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ECommerce.Areas.Management.Models.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ECommerce.Areas.Management.Models.Context.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            List<role> roleList = new List<role>();
            roleList.Add(
                new role()
                {
                    roleId = 1,
                    roleName = "Admin"
                });
            roleList.Add(new role()
                {
                    roleId = 2,
                    roleName = "Customer"
                }
            );
            roleList.ForEach(rolename => context.Role.AddOrUpdate(rolename));
            context.SaveChanges();
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
