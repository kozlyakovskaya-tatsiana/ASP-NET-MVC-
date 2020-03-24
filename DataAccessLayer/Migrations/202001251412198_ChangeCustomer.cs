namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCustomer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "FullName", c => c.String(nullable: false, maxLength: 35));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "FullName", c => c.String(nullable: false));
        }
    }
}
