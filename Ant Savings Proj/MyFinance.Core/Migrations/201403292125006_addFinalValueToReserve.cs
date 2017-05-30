namespace MyFinance.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFinalValueToReserve : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reserves", "FinalExpectedValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reserves", "FinalExpectedValue");
        }
    }
}
