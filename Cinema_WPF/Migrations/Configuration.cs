namespace Cinema_WPF.Migrations
{
    using Cinema_WPF.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Cinema_WPF.Context.CinemaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Cinema_WPF.Context.CinemaContext";
        }

        protected override void Seed(Cinema_WPF.Context.CinemaContext context)
        {

            var AdminRole = new UserRole() { Name = "Admin" };
            context.UserRoles.Add(AdminRole);
            context.SaveChanges();
            var CashierRole = new UserRole() { Name = "Cashier" };

            context.UserRoles.Add(CashierRole);

            context.SaveChanges();
            User AdminUser = new User() { Login = "Admin", Name = "Admin", Surname = "Admin", Password = "Admin", UserRole = context.UserRoles.Find(AdminRole), UserRoleId = 0, DateOfBirth = Convert.ToDateTime(new DateTime(1997, 3, 12)).Date };

            //UserRole AdminRole = new UserRole() { Name = "Admin" };
            //UserRole CashierRole = new UserRole() { Name = "Cashier" };
            //dbContext.UserRoles.Add(AdminRole);
            //dbContext.UserRoles.Add(CashierRole);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
