namespace MyFinance.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixUndoFunc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "OperationId", "dbo.Operations");
            DropIndex("dbo.Transactions", new[] { "OperationId" });
            AlterColumn("dbo.DistributionPercentages", "Percentage", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Transactions", "OperationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "OperationId");
            AddForeignKey("dbo.Transactions", "OperationId", "dbo.Operations", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "OperationId", "dbo.Operations");
            DropIndex("dbo.Transactions", new[] { "OperationId" });
            AlterColumn("dbo.Transactions", "OperationId", c => c.Int());
            AlterColumn("dbo.DistributionPercentages", "Percentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Transactions", "OperationId");
            AddForeignKey("dbo.Transactions", "OperationId", "dbo.Operations", "ID");
        }
    }
}
