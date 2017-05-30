namespace MyFinance.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class increaseValuePrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "Value", c => c.Decimal(nullable: false, precision: 20, scale: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
