namespace BSG.ADInventory.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DatabaseConfiguration : System.Data.Entity.CreateDatabaseIfNotExists<ADInventoryContext> //DbMigrationsConfiguration<PSPContext> 
    {
        //public DatabaseConfiguration()
        //{
        //    AutomaticMigrationsEnabled = true;
        //    AutomaticMigrationDataLossAllowed = true;
        //}

        protected override void Seed(ADInventoryContext context)
        {
            //context.Database.ExecuteSqlCommand("Alter Table Users Drop Column Code Alter Table Users Add Code BigInt Identity(1000, 1)");
            this.Initialize(context);
            base.Seed(context);
        }

        private void Initialize(ADInventoryContext context)
        {
            //this.InitializeBanks(context);
            //this.InitializeSecurity(context);
        }
    }
}
