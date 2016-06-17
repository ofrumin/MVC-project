namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.ModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DAL.ModelContext";
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DAL.ModelContext context)
        {
           
        }
    }
}
