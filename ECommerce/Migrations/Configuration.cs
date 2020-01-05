namespace ECommerce.Migrations
{
    using ECommerce.Areas.Management.Models.Entities;
    using System;
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
            
            context.Role.AddOrUpdate(new role()
            {
                roleName="Admin"
            },
            new role()
            {
                roleName="User"
            }
            );
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
