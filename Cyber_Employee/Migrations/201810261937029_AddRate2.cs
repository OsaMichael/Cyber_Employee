namespace Cyber_Employee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRate2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RatePercentage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Amount);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rates");
        }
    }
}
