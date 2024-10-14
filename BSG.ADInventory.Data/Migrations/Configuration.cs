
namespace BSG.ADInventory.Data.Migrations
{
    using BSG.ADInventory.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Zcf.Security;

    public sealed class Configuration : DbMigrationsConfiguration<Data.ADInventoryContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled =true;
            this.AutomaticMigrationDataLossAllowed = true;
                
        }

        protected override void Seed(ADInventoryContext context)
        {
            //context.Database.ExecuteSqlCommand("Alter Table Users Drop Column Code Alter Table Users Add Code BigInt Identity(1000, 1)");
            this.Initialize(context);
            this.InitializeBaseData(context);
            base.Seed(context);
        }

        private void Initialize(ADInventoryContext context)
        {
            if (context.Users.Any(u => u.UserName == Common.Security.BuiltinUsers.Admin))
            {
                return;
            }

            this.InitializeRoles(context);
            this.InitializeSecurity(context);
        }

        private void InitializeRoles(ADInventoryContext context)
        {
            // Security role
            var securityRole = new Entities.Role { Name = Common.Security.BuiltinRoles.Admin };
            var securityPermissions = new[]
                                      {
                                          Common.Security.Permissions.UserManagement,
                                          Common.Security.Permissions.RoleManagement
                                      };
            foreach (var securityPermission in securityPermissions)
            {
                securityRole.RolesInPermissions.Add(new Entities.RolesInPermission { PermissionId = securityPermission.PermissionId });
            }

            context.Roles.Add(securityRole);
            context.SaveChanges();
        }

        private void InitializeSecurity(ADInventoryContext context)
        {
            var adminUser = new Entities.User
            {
                UserName = Common.Security.BuiltinUsers.Admin,
                Email = "Nabiloo.Reza@gmail.com",
                Type = 1,
                IsActive = true
                //,                
                //FirstName = "مدیر",
                //LastName = "سیستم",
                
            };

            adminUser.SetHashedPassword("admin");

            var adminRole = new Entities.Role
            {
                IsBuiltin = true,
                Name = Common.Security.BuiltinRoles.Admin,
            };

            foreach (var permission in PermissionContainer.Permissions)
            {
                adminRole.RolesInPermissions.Add(new Entities.RolesInPermission { PermissionId = permission.PermissionId });
            }

            adminUser.Roles.Add(adminRole);
            context.Users.Add(adminUser);
            try
            {
                context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                throw ex;
            }
        }


        private void InitializeBaseData(ADInventoryContext context)
        {

            context.InvDocTypes.AddOrUpdate(
               evt => evt.Id,
               new InvDocType() { Id = 1, Title= "گزارش ورود خارجی", Sign=1, IsActive=true, DocPrefix="A" },
               new InvDocType() { Id = 2, Title = "گزارش ورود داخلی", Sign = 1, IsActive = true, DocPrefix = "B" },
               new InvDocType() { Id = 3, Title = "گزارش ورود مستقیم", Sign = 0, IsActive = true, DocPrefix = "C" },
               new InvDocType() { Id = 4, Title = "حواله مصرف", Sign = -1, IsActive = true, DocPrefix = "D" },
               new InvDocType() { Id = 5, Title = "برگشت به انبار", Sign = 1, IsActive = true , DocPrefix = "E" },
               new InvDocType() { Id = 6, Title = "بارنامه داخلی", Sign = -1, IsActive = true , DocPrefix = "F" },
               new InvDocType() { Id = 7, Title = "بارنامه خارجی", Sign = -1, IsActive = true , DocPrefix = "G" }
               );

            context.SaveChanges();
        }
    }
}