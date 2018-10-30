namespace Cyber_Employee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIDToRate : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Rates");
            AddColumn("dbo.Rates", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Rates", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Rates");
            DropColumn("dbo.Rates", "Id");
            AddPrimaryKey("dbo.Rates", "Amount");
        }
    }
}
