namespace MyFinance.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFundType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funds", "FundType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Funds", "FundType");
        }
    }
}
