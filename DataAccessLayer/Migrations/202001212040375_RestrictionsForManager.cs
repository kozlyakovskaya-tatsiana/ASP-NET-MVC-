namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestrictionsForManager : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Managers", "Name", c => c.String(nullable: false, maxLength: 35));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Managers", "Name", c => c.String(nullable: false));
        }
    }
}
