namespace MyFinance.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createOperationEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Profits", "FundID", "dbo.Funds");
            DropForeignKey("dbo.Transactions", "ProfitId", "dbo.Profits");
            DropForeignKey("dbo.Transactions", "FundOriginID", "dbo.Funds");
            DropIndex("dbo.Profits", new[] { "FundID" });
            DropIndex("dbo.Transactions", new[] { "ProfitId" });
            DropIndex("dbo.Transactions", new[] { "FundOriginID" });
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TotalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Transactions", "OperationId", c => c.Int());
            AlterColumn("dbo.Reserves", "TimeUnit", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "OperationId");
            AddForeignKey("dbo.Transactions", "OperationId", "dbo.Operations", "ID");
            DropColumn("dbo.Transactions", "Type");
            DropColumn("dbo.Transactions", "Date");
            DropColumn("dbo.Transactions", "Origins");
            DropColumn("dbo.Transactions", "FundOriginID");
            DropColumn("dbo.Transactions", "ProfitId");
            DropColumn("dbo.Transactions", "UserId");
            DropTable("dbo.Profits");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Profits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        FundID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Transactions", "UserId", c => c.String());
            AddColumn("dbo.Transactions", "ProfitId", c => c.Int());
            AddColumn("dbo.Transactions", "FundOriginID", c => c.Int());
            AddColumn("dbo.Transactions", "Origins", c => c.String());
            AddColumn("dbo.Transactions", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transactions", "Type", c => c.Int(nullable: false));
            DropForeignKey("dbo.Transactions", "OperationId", "dbo.Operations");
            DropIndex("dbo.Transactions", new[] { "OperationId" });
            AlterColumn("dbo.Reserves", "TimeUnit", c => c.String());
            DropColumn("dbo.Transactions", "OperationId");
            DropTable("dbo.Operations");
            CreateIndex("dbo.Transactions", "FundOriginID");
            CreateIndex("dbo.Transactions", "ProfitId");
            CreateIndex("dbo.Profits", "FundID");
            AddForeignKey("dbo.Transactions", "FundOriginID", "dbo.Funds", "ID");
            AddForeignKey("dbo.Transactions", "ProfitId", "dbo.Profits", "ID");
            AddForeignKey("dbo.Profits", "FundID", "dbo.Funds", "ID", cascadeDelete: true);
        }
    }
}
