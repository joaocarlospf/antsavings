namespace MyFinance.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixfundType : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [dbo].[Funds] SET FundType = 5 WHERE FundType IS NULL");
            AlterColumn("dbo.Funds", "FundType", c => c.Int(nullable: false, defaultValue: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Funds", "FundType", c => c.Int());
        }
    }
}
