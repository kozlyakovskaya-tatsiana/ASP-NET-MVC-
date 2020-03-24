using System.Data.Entity.Migrations;

namespace DataAccessLayer.Migrations
{
   

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.ContextModels.SalesDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccessLayer.ContextModels.SalesDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
