namespace MyFinance.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddateWithdrawfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reserves", "DateToWithdraw", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reserves", "DateToWithdraw");
        }
    }
}
