namespace MyFinance.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DistributionPercentages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReserveID = c.Int(nullable: false),
                        FundID = c.Int(),
                        Percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DistributionRuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DistributionRules", t => t.DistributionRuleId, cascadeDelete: true)
                .ForeignKey("dbo.Funds", t => t.FundID)
                .ForeignKey("dbo.Reserves", t => t.ReserveID, cascadeDelete: true)
                .Index(t => t.DistributionRuleId)
                .Index(t => t.FundID)
                .Index(t => t.ReserveID);
            
            CreateTable(
                "dbo.DistributionRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Funds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Profits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        FundID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Funds", t => t.FundID, cascadeDelete: true)
                .Index(t => t.FundID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Type = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ReserveID = c.Int(nullable: false),
                        FundID = c.Int(nullable: false),
                        Origins = c.String(),
                        FundOriginID = c.Int(),
                        ProfitId = c.Int(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profits", t => t.ProfitId)
                .ForeignKey("dbo.Reserves", t => t.ReserveID, cascadeDelete: true)
                .ForeignKey("dbo.Funds", t => t.FundID, cascadeDelete: true)
                .ForeignKey("dbo.Funds", t => t.FundOriginID)
                .Index(t => t.ProfitId)
                .Index(t => t.ReserveID)
                .Index(t => t.FundID)
                .Index(t => t.FundOriginID);
            
            CreateTable(
                "dbo.Reserves",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PeriodToWithdraw = c.Int(nullable: false),
                        TimeUnit = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DistributionPercentages", "ReserveID", "dbo.Reserves");
            DropForeignKey("dbo.DistributionPercentages", "FundID", "dbo.Funds");
            DropForeignKey("dbo.Transactions", "FundOriginID", "dbo.Funds");
            DropForeignKey("dbo.Transactions", "FundID", "dbo.Funds");
            DropForeignKey("dbo.Transactions", "ReserveID", "dbo.Reserves");
            DropForeignKey("dbo.Transactions", "ProfitId", "dbo.Profits");
            DropForeignKey("dbo.Profits", "FundID", "dbo.Funds");
            DropForeignKey("dbo.DistributionPercentages", "DistributionRuleId", "dbo.DistributionRules");
            DropIndex("dbo.DistributionPercentages", new[] { "ReserveID" });
            DropIndex("dbo.DistributionPercentages", new[] { "FundID" });
            DropIndex("dbo.Transactions", new[] { "FundOriginID" });
            DropIndex("dbo.Transactions", new[] { "FundID" });
            DropIndex("dbo.Transactions", new[] { "ReserveID" });
            DropIndex("dbo.Transactions", new[] { "ProfitId" });
            DropIndex("dbo.Profits", new[] { "FundID" });
            DropIndex("dbo.DistributionPercentages", new[] { "DistributionRuleId" });
            DropTable("dbo.Reserves");
            DropTable("dbo.Transactions");
            DropTable("dbo.Profits");
            DropTable("dbo.Funds");
            DropTable("dbo.DistributionRules");
            DropTable("dbo.DistributionPercentages");
        }
    }
}
